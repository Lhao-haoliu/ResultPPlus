using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ResultPPlus
{
    public partial class BatchForm : Form
    {
        private readonly List<BatchResult> _results = new List<BatchResult>();

        public BatchForm()
        {
            InitializeComponent();
            rbLitho.Checked = true;
            lblStatus.Text = "就绪";
            chartCompare.Visible = false;
            btnCompare.Enabled = false;
            tabBottom.SelectedTab = tabPageDetail;
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "选择包含 xlsx 文件的文件夹";
                dialog.ShowNewFolderButton = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtFolder.Text = dialog.SelectedPath;
                    lblStatus.Text = "已选择文件夹：" + dialog.SelectedPath;
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            var folder = txtFolder.Text?.Trim();
            if (string.IsNullOrWhiteSpace(folder) || !Directory.Exists(folder))
            {
                MessageBox.Show("请先选择一个有效的文件夹。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var files = Directory.GetFiles(folder, "*.xlsx")
                .OrderBy(f => File.GetLastWriteTime(f))
                .ThenBy(f => f)
                .ToList();
            if (files.Count == 0)
            {
                MessageBox.Show("该文件夹内没有找到 xlsx 文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                btnProcess.Enabled = false;
                btnCompare.Enabled = false;
                chartCompare.Visible = false;
                dgvSummary.Rows.Clear();
                dgvSummary.Columns.Clear();
                dgvDetail.Rows.Clear();
                dgvDetail.Columns.Clear();
                _results.Clear();

                lblStatus.Text = "批量统计中...";

                AppConfig cfg = null;
                if (rbIntegration.Checked)
                {
                    var cfgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
                    cfg = AppConfig.LoadOrDefault(cfgPath);
                }

                ConfigureSummaryGrid(cfg);
                ConfigureDetailGrid();

                int index = 0;
                foreach (var file in files)
                {
                    index++;
                    lblStatus.Text = $"处理中 {index}/{files.Count}：{Path.GetFileName(file)}";
                    Application.DoEvents();

                    try
                    {
                        List<StatRow> rows;
                        List<ErrorSummaryRow> errorSummary = new List<ErrorSummaryRow>();
                        if (rbLitho.Checked)
                        {
                            rows = StatServices.CalcLitho(file);
                        }
                        else
                        {
                            var detail = StatServices.CalcIntegrationDetailed(file, cfg);
                            rows = detail.Rows;
                            errorSummary = detail.ErrorSummary;
                        }

                        _results.Add(new BatchResult
                        {
                            FilePath = file,
                            Rows = rows,
                            ErrorSummary = errorSummary
                        });

                        AppendSummaryRow(file, rows);
                    }
                    catch (Exception ex)
                    {
                        dgvSummary.Rows.Add(Path.GetFileName(file), "错误");
                        lblStatus.Text = "部分文件失败";
                        MessageBox.Show($"文件处理失败：{Path.GetFileName(file)}\r\n{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (_results.Count > 0)
                {
                    btnCompare.Enabled = true;
                }

                lblStatus.Text = "批量统计完成";
            }
            finally
            {
                btnProcess.Enabled = true;
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (_results.Count == 0)
            {
                MessageBox.Show("请先完成批量统计。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            chartCompare.Series.Clear();
            chartCompare.ChartAreas.Clear();
            chartCompare.Legends.Clear();

            var chartArea = new ChartArea("main");
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Maximum = 100;
            chartArea.AxisY.Interval = 10;
            chartArea.AxisY.CustomLabels.Clear();
            chartArea.AxisY.MajorGrid.Interval = 10;
            chartArea.AxisY.MinorGrid.Enabled = false;
            chartArea.AxisY.LabelStyle.Enabled = false;
            chartArea.AxisY.Title = "成功率 (%)";
            chartCompare.ChartAreas.Add(chartArea);

            var legend = new Legend();
            legend.Docking = Docking.Top;
            chartCompare.Legends.Add(legend);

            double TransformRate(double value)
            {
                if (value <= 70)
                {
                    return value * (50.0 / 70.0);
                }
                return 50.0 + (value - 70.0) * (50.0 / 30.0);
            }

            void AddAxisLabel(double actualValue)
            {
                var pos = TransformRate(actualValue);
                var label = new CustomLabel(pos - 1, pos + 1, actualValue.ToString("F0"), 0, LabelMarkStyle.None);
                chartArea.AxisY.CustomLabels.Add(label);
            }

            foreach (var tick in new[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 })
            {
                AddAxisLabel(tick);
            }

            var allTypes = _results
                .SelectMany(r => r.Rows)
                .Select(r => r.Type)
                .Where(t => !string.Equals(t, "总计", StringComparison.OrdinalIgnoreCase))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(t => t)
                .ToList();

            foreach (var type in allTypes)
            {
                var series = new Series(type)
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 6
                };

                foreach (var result in _results)
                {
                    var row = result.Rows.FirstOrDefault(r => string.Equals(r.Type, type, StringComparison.OrdinalIgnoreCase));
                    if (row == null) continue;

                    var pointIndex = series.Points.AddY(TransformRate(row.SuccessRateValue));
                    series.Points[pointIndex].AxisLabel = result.FileName;
                    series.Points[pointIndex].Label = $"{row.SuccessRateValue:F2}%";
                }

                chartCompare.Series.Add(series);
            }

            chartCompare.Visible = true;
            tabBottom.SelectedTab = tabPageChart;
        }

        private void ConfigureSummaryGrid(AppConfig cfg)
        {
            dgvSummary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvSummary.Columns.Clear();

            if (rbLitho.Checked)
            {
                dgvSummary.Columns.Add("File", "文件");
                dgvSummary.Columns.Add("Type", "类型");
                dgvSummary.Columns.Add("TrueCount", "TRUE");
                dgvSummary.Columns.Add("FalseCount", "FALSE");
                dgvSummary.Columns.Add("Total", "TOTAL");
                dgvSummary.Columns.Add("SuccessRate", "成功率");

                dgvSummary.Columns["File"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSummary.Columns["Type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvSummary.Columns["TrueCount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvSummary.Columns["FalseCount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvSummary.Columns["Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvSummary.Columns["SuccessRate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            else
            {
                cfg = cfg ?? AppConfig.Default();
                if (cfg.Integration == null) cfg.Integration = AppConfig.Default().Integration;
                if (cfg.Integration.Mappings == null) cfg.Integration.Mappings = AppConfig.Default().Integration.Mappings;

                dgvSummary.Columns.Add("File", "文件");
                dgvSummary.Columns.Add("TotalFalse", "总失败数");

                foreach (var item in cfg.Integration.Mappings.Keys)
                {
                    dgvSummary.Columns.Add(item, item + " 失败");
                }

                dgvSummary.Columns["File"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSummary.Columns["TotalFalse"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                foreach (DataGridViewColumn column in dgvSummary.Columns)
                {
                    if (column.Name == "File")
                    {
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }
                    else
                    {
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                }
            }

            dgvSummary.Columns["File"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvSummary.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        private void ConfigureDetailGrid()
        {
            dgvDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvDetail.Columns.Clear();
            dgvDetail.Columns.Add("Item", "检测项");
            dgvDetail.Columns.Add("Type", "类型");
            dgvDetail.Columns.Add("Count", "次数");

            dgvDetail.Columns["Item"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDetail.Columns["Type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvDetail.Columns["Count"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvDetail.Columns["Item"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDetail.Columns["Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDetail.Columns["Count"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetail.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        private void AppendSummaryRow(string file, List<StatRow> rows)
        {
            if (rbLitho.Checked)
            {
                foreach (var row in rows)
                {
                    dgvSummary.Rows.Add(Path.GetFileName(file), row.Type, row.TrueCount, row.FalseCount, row.Total, row.SuccessRate);
                }
                return;
            }

            var fileName = Path.GetFileName(file);
            var itemRows = rows.Where(r => !string.Equals(r.Type, "总计", StringComparison.OrdinalIgnoreCase)).ToList();
            var totalFalse = itemRows.Sum(r => r.FalseCount);
            var values = new List<object> { fileName, totalFalse };
            foreach (var item in dgvSummary.Columns.Cast<DataGridViewColumn>().Skip(2))
            {
                var row = itemRows.FirstOrDefault(r => string.Equals(r.Type, item.Name, StringComparison.OrdinalIgnoreCase));
                values.Add(row?.FalseCount ?? 0);
            }
            dgvSummary.Rows.Add(values.ToArray());
        }

        private void dgvSummary_SelectionChanged(object sender, EventArgs e)
        {
            if (rbLitho.Checked)
            {
                dgvDetail.Rows.Clear();
                return;
            }

            if (dgvSummary.SelectedRows.Count == 0) return;
            var fileName = dgvSummary.SelectedRows[0].Cells[0].Value?.ToString();
            if (string.IsNullOrWhiteSpace(fileName)) return;

            var match = _results.FirstOrDefault(r => string.Equals(r.FileName, fileName, StringComparison.OrdinalIgnoreCase));
            dgvDetail.Rows.Clear();
            if (match == null || match.ErrorSummary == null || match.ErrorSummary.Count == 0) return;

            foreach (var item in match.ErrorSummary)
            {
                dgvDetail.Rows.Add(item.Item, item.Type, item.Count);
            }
        }

        private class BatchResult
        {
            public string FilePath { get; set; }
            public string FileName => Path.GetFileName(FilePath);
            public List<StatRow> Rows { get; set; }
            public List<ErrorSummaryRow> ErrorSummary { get; set; }
        }
    }
}
