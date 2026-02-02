
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ResultPPlus
{
    public partial class MainForm : Form
    {
        private string _filePath;

        public MainForm()
        {
            InitializeComponent();
            rbLitho.Checked = true;
            lblStatus.Text = "就绪";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel 文件 (*.xlsx)|*.xlsx";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _filePath = ofd.FileName;
                    txtFile.Text = _filePath;
                    lblStatus.Text = "已选择：" + Path.GetFileName(_filePath);
                }
            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_filePath) || !File.Exists(_filePath))
            {
                MessageBox.Show("请先选择一个 xlsx 文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                btnCalc.Enabled = false;
                lblStatus.Text = "统计中...";
                dgv.Rows.Clear();

                List<StatRow> rows;
                List<ErrorSummaryRow> errorSummary = new List<ErrorSummaryRow>();

                if (rbLitho.Checked)
                {
                    rows = StatServices.CalcLitho(_filePath);
                    dgvErrors.Rows.Clear();
                }
                else
                {
                    var cfgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
                    var cfg = AppConfig.LoadOrDefault(cfgPath);
                    var detail = StatServices.CalcIntegrationDetailed(_filePath, cfg);
                    rows = detail.Rows;
                    errorSummary = detail.ErrorSummary;
                }

                foreach (var r in rows)
                {
                    dgv.Rows.Add(r.Type, r.TrueCount, r.FalseCount, r.Total, r.SuccessRate);
                }

                dgvErrors.Rows.Clear();
                foreach (var item in errorSummary)
                {
                    dgvErrors.Rows.Add(item.Item, item.Type, item.Count);
                }

                // 总计行加粗
                if (dgv.Rows.Count > 0)
                {
                    dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.Font =
                        new System.Drawing.Font(dgv.Font, System.Drawing.FontStyle.Bold);
                }

                lblStatus.Text = "统计完成";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "发生错误";
                MessageBox.Show("统计失败：\r\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCalc.Enabled = true;
            }
        }
    }
}
