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
                dgv.Rows.Clear();
                _results.Clear();

                lblStatus.Text = "批量统计中...";

                AppConfig cfg = null;
                if (rbIntegration.Checked)
                {
                    var cfgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
                    cfg = AppConfig.LoadOrDefault(cfgPath);
                }

                int index = 0;
                foreach (var file in files)
                {
                    index++;
                    lblStatus.Text = $"处理中 {index}/{files.Count}：{Path.GetFileName(file)}";
                    Application.DoEvents();

                    try
                    {
                        List<StatRow> rows;
                        if (rbLitho.Checked)
                        {
                            rows = StatServices.CalcLitho(file);
                        }
                        else
                        {
                            rows = StatServices.CalcIntegration(file, cfg);
                        }

                        _results.Add(new BatchResult
                        {
                            FilePath = file,
                            Rows = rows
                        });

                        foreach (var row in rows)
                        {
                            dgv.Rows.Add(Path.GetFileName(file), row.Type, row.TrueCount, row.FalseCount, row.Total, row.SuccessRate);
                        }
                    }
                    catch (Exception ex)
                    {
                        dgv.Rows.Add(Path.GetFileName(file), "错误", 0, 0, 0, "-");
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
        }

        private class BatchResult
        {
            public string FilePath { get; set; }
            public string FileName => Path.GetFileName(FilePath);
            public List<StatRow> Rows { get; set; }
        }
    }
}
