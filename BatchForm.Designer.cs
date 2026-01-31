namespace ResultPPlus
{
    partial class BatchForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.GroupBox groupBoxType;
        private System.Windows.Forms.RadioButton rbLitho;
        private System.Windows.Forms.RadioButton rbIntegration;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label labelFolder;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCompare;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.groupBoxType = new System.Windows.Forms.GroupBox();
            this.rbIntegration = new System.Windows.Forms.RadioButton();
            this.rbLitho = new System.Windows.Forms.RadioButton();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.labelFolder = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.chartCompare = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.groupBoxType.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCompare)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnCompare);
            this.panelTop.Controls.Add(this.btnProcess);
            this.panelTop.Controls.Add(this.groupBoxType);
            this.panelTop.Controls.Add(this.btnBrowseFolder);
            this.panelTop.Controls.Add(this.txtFolder);
            this.panelTop.Controls.Add(this.labelFolder);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 120);
            this.panelTop.TabIndex = 0;
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(880, 62);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(280, 40);
            this.btnCompare.TabIndex = 5;
            this.btnCompare.Text = "进一步比较";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(580, 62);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(280, 40);
            this.btnProcess.TabIndex = 4;
            this.btnProcess.Text = "开始批量处理";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // groupBoxType
            // 
            this.groupBoxType.Controls.Add(this.rbIntegration);
            this.groupBoxType.Controls.Add(this.rbLitho);
            this.groupBoxType.Location = new System.Drawing.Point(80, 54);
            this.groupBoxType.Name = "groupBoxType";
            this.groupBoxType.Size = new System.Drawing.Size(360, 52);
            this.groupBoxType.TabIndex = 3;
            this.groupBoxType.TabStop = false;
            this.groupBoxType.Text = "类型选择";
            // 
            // rbIntegration
            // 
            this.rbIntegration.AutoSize = true;
            this.rbIntegration.Location = new System.Drawing.Point(160, 22);
            this.rbIntegration.Name = "rbIntegration";
            this.rbIntegration.Size = new System.Drawing.Size(71, 16);
            this.rbIntegration.TabIndex = 1;
            this.rbIntegration.Text = "整合类";
            this.rbIntegration.UseVisualStyleBackColor = true;
            // 
            // rbLitho
            // 
            this.rbLitho.AutoSize = true;
            this.rbLitho.Location = new System.Drawing.Point(28, 22);
            this.rbLitho.Name = "rbLitho";
            this.rbLitho.Size = new System.Drawing.Size(71, 16);
            this.rbLitho.TabIndex = 0;
            this.rbLitho.Text = "黄光类";
            this.rbLitho.UseVisualStyleBackColor = true;
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Location = new System.Drawing.Point(1030, 16);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(130, 28);
            this.btnBrowseFolder.TabIndex = 2;
            this.btnBrowseFolder.Text = "选择文件夹";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(80, 18);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(940, 21);
            this.txtFolder.TabIndex = 1;
            // 
            // labelFolder
            // 
            this.labelFolder.Location = new System.Drawing.Point(12, 20);
            this.labelFolder.Name = "labelFolder";
            this.labelFolder.Size = new System.Drawing.Size(70, 18);
            this.labelFolder.TabIndex = 0;
            this.labelFolder.Text = "文件夹：";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.chartCompare);
            this.panelMain.Controls.Add(this.dgv);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 120);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1200, 540);
            this.panelMain.TabIndex = 1;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1200, 300);
            this.dgv.TabIndex = 0;
            // 
            // chartCompare
            // 
            this.chartCompare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartCompare.Location = new System.Drawing.Point(0, 300);
            this.chartCompare.Name = "chartCompare";
            this.chartCompare.Size = new System.Drawing.Size(1200, 240);
            this.chartCompare.TabIndex = 1;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.lblStatus);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 660);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1200, 30);
            this.panelBottom.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblStatus.Size = new System.Drawing.Size(1200, 30);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "就绪";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BatchForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 690);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "BatchForm";
            this.Text = "批量正确率统计";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.groupBoxType.ResumeLayout(false);
            this.groupBoxType.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCompare)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

            // DataGridView 列
            this.dgv.Columns.Clear();
            this.dgv.Columns.Add("File", "文件");
            this.dgv.Columns.Add("Type", "类型");
            this.dgv.Columns.Add("TrueCount", "TRUE");
            this.dgv.Columns.Add("FalseCount", "FALSE");
            this.dgv.Columns.Add("Total", "TOTAL");
            this.dgv.Columns.Add("SuccessRate", "成功率");

            this.dgv.Columns["File"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgv.Columns["Type"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv.Columns["TrueCount"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv.Columns["FalseCount"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv.Columns["Total"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv.Columns["SuccessRate"].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;

            this.dgv.Columns["File"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgv.Columns["Type"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv.Columns["TrueCount"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgv.Columns["FalseCount"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgv.Columns["Total"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgv.Columns["SuccessRate"].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
        }
    }
}
