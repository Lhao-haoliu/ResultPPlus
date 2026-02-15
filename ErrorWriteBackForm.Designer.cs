namespace ResultPPlus
{
    partial class ErrorWriteBackForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnBrowseLog;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnBrowseResult;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.DataGridView dgvMapping;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.Label labelSeparator;
        private System.Windows.Forms.TextBox txtSeparator;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.CheckBox chkClearValidation;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.GroupBox groupBoxMapping;
        private System.Windows.Forms.GroupBox groupBoxResults;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.chkClearValidation = new System.Windows.Forms.CheckBox();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.txtSeparator = new System.Windows.Forms.TextBox();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.labelOutput = new System.Windows.Forms.Label();
            this.btnBrowseResult = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.labelResult = new System.Windows.Forms.Label();
            this.btnBrowseLog = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.labelLog = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.groupBoxMapping = new System.Windows.Forms.GroupBox();
            this.dgvMapping = new System.Windows.Forms.DataGridView();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.groupBoxMapping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapping)).BeginInit();
            this.groupBoxResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnStart);
            this.panelTop.Controls.Add(this.chkClearValidation);
            this.panelTop.Controls.Add(this.chkOverwrite);
            this.panelTop.Controls.Add(this.txtSeparator);
            this.panelTop.Controls.Add(this.labelSeparator);
            this.panelTop.Controls.Add(this.btnBrowseOutput);
            this.panelTop.Controls.Add(this.txtOutput);
            this.panelTop.Controls.Add(this.labelOutput);
            this.panelTop.Controls.Add(this.btnBrowseResult);
            this.panelTop.Controls.Add(this.txtResult);
            this.panelTop.Controls.Add(this.labelResult);
            this.panelTop.Controls.Add(this.btnBrowseLog);
            this.panelTop.Controls.Add(this.txtLog);
            this.panelTop.Controls.Add(this.labelLog);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1100, 150);
            this.panelTop.TabIndex = 0;
            // 
            // chkClearValidation
            // 
            this.chkClearValidation.AutoSize = true;
            this.chkClearValidation.Location = new System.Drawing.Point(670, 94);
            this.chkClearValidation.Name = "chkClearValidation";
            this.chkClearValidation.Size = new System.Drawing.Size(96, 16);
            this.chkClearValidation.TabIndex = 12;
            this.chkClearValidation.Text = "清除校验";
            this.chkClearValidation.UseVisualStyleBackColor = true;
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.Location = new System.Drawing.Point(670, 68);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(72, 16);
            this.chkOverwrite.TabIndex = 11;
            this.chkOverwrite.Text = "覆盖原";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // txtSeparator
            // 
            this.txtSeparator.Location = new System.Drawing.Point(520, 92);
            this.txtSeparator.Name = "txtSeparator";
            this.txtSeparator.Size = new System.Drawing.Size(120, 21);
            this.txtSeparator.TabIndex = 10;
            // 
            // labelSeparator
            // 
            this.labelSeparator.Location = new System.Drawing.Point(440, 94);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(80, 18);
            this.labelSeparator.TabIndex = 9;
            this.labelSeparator.Text = "分隔符：";
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Location = new System.Drawing.Point(960, 90);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(110, 24);
            this.btnBrowseOutput.TabIndex = 8;
            this.btnBrowseOutput.Text = "选择输出";
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(120, 92);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(310, 21);
            this.txtOutput.TabIndex = 7;
            // 
            // labelOutput
            // 
            this.labelOutput.Location = new System.Drawing.Point(12, 94);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(100, 18);
            this.labelOutput.TabIndex = 6;
            this.labelOutput.Text = "输出路径：";
            // 
            // btnBrowseResult
            // 
            this.btnBrowseResult.Location = new System.Drawing.Point(960, 54);
            this.btnBrowseResult.Name = "btnBrowseResult";
            this.btnBrowseResult.Size = new System.Drawing.Size(110, 24);
            this.btnBrowseResult.TabIndex = 5;
            this.btnBrowseResult.Text = "选择结果";
            this.btnBrowseResult.UseVisualStyleBackColor = true;
            this.btnBrowseResult.Click += new System.EventHandler(this.btnBrowseResult_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(120, 56);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(820, 21);
            this.txtResult.TabIndex = 4;
            // 
            // labelResult
            // 
            this.labelResult.Location = new System.Drawing.Point(12, 58);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(100, 18);
            this.labelResult.TabIndex = 3;
            this.labelResult.Text = "结果文件：";
            // 
            // btnBrowseLog
            // 
            this.btnBrowseLog.Location = new System.Drawing.Point(960, 18);
            this.btnBrowseLog.Name = "btnBrowseLog";
            this.btnBrowseLog.Size = new System.Drawing.Size(110, 24);
            this.btnBrowseLog.TabIndex = 2;
            this.btnBrowseLog.Text = "选择日志";
            this.btnBrowseLog.UseVisualStyleBackColor = true;
            this.btnBrowseLog.Click += new System.EventHandler(this.btnBrowseLog_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(120, 20);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(820, 21);
            this.txtLog.TabIndex = 1;
            // 
            // labelLog
            // 
            this.labelLog.Location = new System.Drawing.Point(12, 22);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(100, 18);
            this.labelLog.TabIndex = 0;
            this.labelLog.Text = "异常日志：";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(800, 84);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(140, 34);
            this.btnStart.TabIndex = 13;
            this.btnStart.Text = "开始回写";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.splitMain);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 150);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1100, 430);
            this.panelMain.TabIndex = 1;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.groupBoxMapping);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.groupBoxResults);
            this.splitMain.Size = new System.Drawing.Size(1100, 430);
            this.splitMain.SplitterDistance = 180;
            this.splitMain.TabIndex = 0;
            // 
            // groupBoxMapping
            // 
            this.groupBoxMapping.Controls.Add(this.dgvMapping);
            this.groupBoxMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxMapping.Location = new System.Drawing.Point(0, 0);
            this.groupBoxMapping.Name = "groupBoxMapping";
            this.groupBoxMapping.Size = new System.Drawing.Size(1100, 180);
            this.groupBoxMapping.TabIndex = 0;
            this.groupBoxMapping.TabStop = false;
            this.groupBoxMapping.Text = "Function 映射";
            // 
            // dgvMapping
            // 
            this.dgvMapping.AllowUserToAddRows = false;
            this.dgvMapping.AllowUserToDeleteRows = false;
            this.dgvMapping.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMapping.Location = new System.Drawing.Point(3, 17);
            this.dgvMapping.Name = "dgvMapping";
            this.dgvMapping.ReadOnly = true;
            this.dgvMapping.RowHeadersVisible = false;
            this.dgvMapping.RowTemplate.Height = 23;
            this.dgvMapping.Size = new System.Drawing.Size(1094, 160);
            this.dgvMapping.TabIndex = 0;
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Controls.Add(this.dgvResults);
            this.groupBoxResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxResults.Location = new System.Drawing.Point(0, 0);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Size = new System.Drawing.Size(1100, 246);
            this.groupBoxResults.TabIndex = 0;
            this.groupBoxResults.TabStop = false;
            this.groupBoxResults.Text = "处理结果";
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.Location = new System.Drawing.Point(3, 17);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowTemplate.Height = 23;
            this.dgvResults.Size = new System.Drawing.Size(1094, 226);
            this.dgvResults.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.progressBar);
            this.panelBottom.Controls.Add(this.lblStatus);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 580);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1100, 30);
            this.panelBottom.TabIndex = 2;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.progressBar.Location = new System.Drawing.Point(820, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(280, 30);
            this.progressBar.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblStatus.Size = new System.Drawing.Size(1100, 30);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "就绪";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ErrorWriteBackForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 610);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "ErrorWriteBackForm";
            this.Text = "异常日志回写";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.groupBoxMapping.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapping)).EndInit();
            this.groupBoxResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

            this.dgvResults.Columns.Clear();
            this.dgvResults.Columns.Add("Layer", "layer");
            this.dgvResults.Columns.Add("Type", "type");
            this.dgvResults.Columns.Add("Function", "function");
            this.dgvResults.Columns.Add("Matched", "写回行数");
            this.dgvResults.Columns.Add("Status", "状态");
            this.dgvResults.Columns.Add("Message", "原因/备注");

            this.dgvResults.Columns["Layer"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvResults.Columns["Type"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvResults.Columns["Function"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvResults.Columns["Matched"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvResults.Columns["Status"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgvResults.Columns["Message"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
