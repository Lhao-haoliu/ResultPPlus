using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace ResultPPlus
{
    public partial class ErrorWriteBackForm : Form
    {
        private AppConfig _config;

        public ErrorWriteBackForm()
        {
            InitializeComponent();
            lblStatus.Text = "就绪";
            progressBar.Value = 0;
            LoadConfig();
        }

        private void LoadConfig()
        {
            var cfgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            _config = AppConfig.LoadOrDefault(cfgPath);

            dgvMapping.Columns.Clear();
            dgvMapping.Columns.Add("Name", "Function");
            dgvMapping.Columns.Add("Range", "Range");
            dgvMapping.Columns.Add("Result", "Result");
            dgvMapping.Columns.Add("MessageTarget", "MessageTarget");

            dgvMapping.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMapping.Columns["Range"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvMapping.Columns["Result"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvMapping.Columns["MessageTarget"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvMapping.Rows.Clear();
            var mappings = _config.ErrorWriteBack?.Functions ?? new List<ErrorWriteBackFunction>();
            foreach (var item in mappings)
            {
                dgvMapping.Rows.Add(item.Name, item.Range, item.Result, string.IsNullOrWhiteSpace(item.MessageTarget) ? item.Result : item.MessageTarget);
            }

            txtSeparator.Text = _config.ErrorWriteBack?.AppendSeparator ?? " | ";
            chkClearValidation.Checked = _config.ErrorWriteBack?.ClearDataValidation ?? true;
        }

        private void btnBrowseLog_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel 文件 (*.xlsx)|*.xlsx";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtLog.Text = ofd.FileName;
                }
            }
        }

        private void btnBrowseResult_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel 文件 (*.xlsx)|*.xlsx";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtResult.Text = ofd.FileName;
                    if (string.IsNullOrWhiteSpace(txtOutput.Text))
                    {
                        txtOutput.Text = BuildDefaultOutputPath(ofd.FileName);
                    }
                }
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel 文件 (*.xlsx)|*.xlsx";
                sfd.FileName = Path.GetFileName(BuildDefaultOutputPath(txtResult.Text));

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    txtOutput.Text = sfd.FileName;
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var logPath = txtLog.Text?.Trim();
            var resultPath = txtResult.Text?.Trim();
            if (string.IsNullOrWhiteSpace(logPath) || !File.Exists(logPath))
            {
                MessageBox.Show("请选择有效的异常日志 Excel。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(resultPath) || !File.Exists(resultPath))
            {
                MessageBox.Show("请选择有效的整合结果 Excel。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var outputPath = chkOverwrite.Checked ? resultPath : txtOutput.Text?.Trim();
            if (!chkOverwrite.Checked && string.IsNullOrWhiteSpace(outputPath))
            {
                MessageBox.Show("请选择输出文件路径。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!chkOverwrite.Checked && string.Equals(Path.GetFullPath(outputPath), Path.GetFullPath(resultPath), StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("输出路径不能与源文件相同，请选择新的输出文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnStart.Enabled = false;
            dgvResults.Rows.Clear();
            lblStatus.Text = "处理中...";

            try
            {
                var separator = string.IsNullOrWhiteSpace(txtSeparator.Text) ? " | " : txtSeparator.Text;
                var config = _config?.ErrorWriteBack ?? AppConfig.Default().ErrorWriteBack;
                config = config ?? AppConfig.Default().ErrorWriteBack;
                config.AppendSeparator = separator;
                config.ClearDataValidation = chkClearValidation.Checked;

                var logEntries = LoadLogEntries(logPath);
                if (logEntries.Count == 0)
                {
                    MessageBox.Show("异常日志中没有可处理的记录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                progressBar.Maximum = logEntries.Count;
                progressBar.Value = 0;

                var writeTargetPath = chkOverwrite.Checked ? resultPath : outputPath;

                using (var wb = new XLWorkbook(resultPath))
                {
                    var sheetName = string.IsNullOrWhiteSpace(config.SheetName) ? "data" : config.SheetName;
                    var ws = wb.Worksheets.FirstOrDefault(s => string.Equals(s.Name, sheetName, StringComparison.OrdinalIgnoreCase));
                    if (ws == null)
                    {
                        foreach (var entry in logEntries)
                        {
                            AddResultRow(entry, 0, "failed", "data sheet 不存在");
                        }
                        lblStatus.Text = "data sheet 不存在";
                        return;
                    }

                    var dataRows = BuildDataRows(ws, config.LayerColumn, config.TypeColumn);
                    var functionMap = BuildFunctionMap(config.Functions);

                    int success = 0, skipped = 0, failed = 0;

                    foreach (var entry in logEntries)
                    {
                        progressBar.Value = Math.Min(progressBar.Maximum, progressBar.Value + 1);
                        lblStatus.Text = $"处理中 {progressBar.Value}/{progressBar.Maximum}";
                        Application.DoEvents();

                        if (!functionMap.TryGetValue(Normalize(entry.Function), out var func))
                        {
                            failed++;
                            AddResultRow(entry, 0, "failed", "function 未配置");
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(entry.ErrorMessage))
                        {
                            skipped++;
                            AddResultRow(entry, 0, "skipped", "errorMessage 为空");
                            continue;
                        }

                        var matches = dataRows.Where(r => string.Equals(r.Layer, entry.Layer, StringComparison.OrdinalIgnoreCase)
                            && string.Equals(r.Type, entry.Type, StringComparison.OrdinalIgnoreCase)).ToList();
                        if (matches.Count == 0)
                        {
                            failed++;
                            AddResultRow(entry, 0, "failed", "未找到匹配行");
                            continue;
                        }

                        int resultCol = ColumnLetterToNumber(func.Result);
                        int messageCol = ColumnLetterToNumber(string.IsNullOrWhiteSpace(func.MessageTarget) ? func.Result : func.MessageTarget);
                        if (resultCol <= 0 || messageCol <= 0)
                        {
                            failed++;
                            AddResultRow(entry, 0, "failed", "配置列无效");
                            continue;
                        }

                        int written = 0;
                        int skippedByResult = 0;
                        foreach (var row in matches)
                        {
                            var resultCell = ws.Cell(row.RowNumber, resultCol);
                            if (!TryParseBoolValue(resultCell.Value, out bool isTrue))
                            {
                                skippedByResult++;
                                continue;
                            }
                            if (isTrue)
                            {
                                skippedByResult++;
                                continue;
                            }

                            var targetCell = ws.Cell(row.RowNumber, messageCol);
                            if (config.ClearDataValidation)
                            {
                                try { targetCell.DataValidation.Clear(); } catch { }
                            }

                            var existing = targetCell.GetString();
                            if (string.IsNullOrWhiteSpace(existing))
                            {
                                targetCell.Value = entry.ErrorMessage;
                            }
                            else
                            {
                                targetCell.Value = existing + separator + entry.ErrorMessage;
                            }

                            written++;
                        }

                        if (written > 0)
                        {
                            success++;
                            AddResultRow(entry, written, "success", "");
                        }
                        else
                        {
                            skipped++;
                            AddResultRow(entry, 0, "skipped", skippedByResult > 0 ? "result 不是 FALSE" : "未写入");
                        }
                    }

                    if (chkOverwrite.Checked)
                    {
                        wb.Save();
                    }
                    else
                    {
                        wb.SaveAs(writeTargetPath);
                    }

                    lblStatus.Text = $"完成：success {success} / skipped {skipped} / failed {failed}";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "发生错误";
                MessageBox.Show("回写失败：\r\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnStart.Enabled = true;
            }
        }

        private List<LogEntry> LoadLogEntries(string path)
        {
            var entries = new List<LogEntry>();
            using (var wb = new XLWorkbook(path))
            {
                var ws = wb.Worksheets.First();
                var headerRow = ws.Row(1);
                var headerMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                foreach (var cell in headerRow.CellsUsed())
                {
                    var key = Normalize(cell.GetString());
                    if (!headerMap.ContainsKey(key))
                        headerMap[key] = cell.Address.ColumnNumber;
                }

                int lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;
                for (int r = 2; r <= lastRow; r++)
                {
                    var layer = GetCellString(ws, r, headerMap, "layer");
                    var type = GetCellString(ws, r, headerMap, "type");
                    var function = GetCellString(ws, r, headerMap, "function");
                    var errorMessage = GetCellString(ws, r, headerMap, "errormessage");

                    if (string.IsNullOrWhiteSpace(layer) && string.IsNullOrWhiteSpace(type) && string.IsNullOrWhiteSpace(function) && string.IsNullOrWhiteSpace(errorMessage))
                        continue;

                    entries.Add(new LogEntry
                    {
                        Layer = layer?.Trim(),
                        Type = type?.Trim(),
                        Function = function?.Trim(),
                        ErrorMessage = errorMessage?.Trim()
                    });
                }
            }
            return entries;
        }

        private static string GetCellString(IXLWorksheet ws, int row, Dictionary<string, int> headerMap, string key)
        {
            if (!headerMap.TryGetValue(key, out int col)) return "";
            return ws.Cell(row, col).GetString();
        }

        private static List<DataRowInfo> BuildDataRows(IXLWorksheet ws, string layerColumn, string typeColumn)
        {
            int layerCol = ColumnLetterToNumber(layerColumn);
            int typeCol = ColumnLetterToNumber(typeColumn);
            var rows = new List<DataRowInfo>();
            if (layerCol <= 0 || typeCol <= 0) return rows;

            int lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;
            string currentLayer = null;
            for (int r = 1; r <= lastRow; r++)
            {
                var layerValue = ws.Cell(r, layerCol).GetString().Trim();
                if (!string.IsNullOrWhiteSpace(layerValue))
                    currentLayer = layerValue;

                var typeValue = ws.Cell(r, typeCol).GetString().Trim();
                if (string.IsNullOrWhiteSpace(currentLayer) && string.IsNullOrWhiteSpace(typeValue))
                    continue;

                rows.Add(new DataRowInfo
                {
                    RowNumber = r,
                    Layer = currentLayer ?? "",
                    Type = typeValue
                });
            }
            return rows;
        }

        private static Dictionary<string, ErrorWriteBackFunction> BuildFunctionMap(List<ErrorWriteBackFunction> functions)
        {
            var map = new Dictionary<string, ErrorWriteBackFunction>(StringComparer.OrdinalIgnoreCase);
            if (functions == null) return map;

            foreach (var func in functions)
            {
                if (string.IsNullOrWhiteSpace(func.Name)) continue;
                var key = Normalize(func.Name);
                if (!map.ContainsKey(key)) map[key] = func;
                if (func.Aliases == null) continue;
                foreach (var alias in func.Aliases)
                {
                    var aliasKey = Normalize(alias);
                    if (!string.IsNullOrWhiteSpace(aliasKey) && !map.ContainsKey(aliasKey))
                        map[aliasKey] = func;
                }
            }

            return map;
        }

        private static int ColumnLetterToNumber(string letters)
        {
            if (string.IsNullOrWhiteSpace(letters)) return -1;
            letters = letters.Trim().ToUpperInvariant();

            int sum = 0;
            foreach (char c in letters)
            {
                if (c < 'A' || c > 'Z') return -1;
                sum = sum * 26 + (c - 'A' + 1);
            }
            return sum;
        }

        private static bool TryParseBoolValue(object v, out bool b)
        {
            b = false;
            if (v == null) return false;

            if (v is bool bb)
            {
                b = bb;
                return true;
            }

            var s = v.ToString();
            if (string.IsNullOrWhiteSpace(s)) return false;
            s = s.Trim();

            if (string.Equals(s, "TRUE", StringComparison.OrdinalIgnoreCase)) { b = true; return true; }
            if (string.Equals(s, "FALSE", StringComparison.OrdinalIgnoreCase)) { b = false; return true; }

            return false;
        }

        private static string Normalize(string s)
        {
            return string.IsNullOrWhiteSpace(s) ? string.Empty : s.Trim().ToLowerInvariant();
        }

        private static string BuildDefaultOutputPath(string resultPath)
        {
            if (string.IsNullOrWhiteSpace(resultPath)) return string.Empty;
            var dir = Path.GetDirectoryName(resultPath);
            var name = Path.GetFileNameWithoutExtension(resultPath);
            var ext = Path.GetExtension(resultPath);
            return Path.Combine(dir ?? string.Empty, name + "_回写" + ext);
        }

        private void AddResultRow(LogEntry entry, int matchedRows, string status, string reason)
        {
            dgvResults.Rows.Add(entry.Layer, entry.Type, entry.Function, matchedRows, status, reason);
        }

        private class LogEntry
        {
            public string Layer { get; set; }
            public string Type { get; set; }
            public string Function { get; set; }
            public string ErrorMessage { get; set; }
        }

        private class DataRowInfo
        {
            public int RowNumber { get; set; }
            public string Layer { get; set; }
            public string Type { get; set; }
        }
    }
}
