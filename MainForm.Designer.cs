namespace ResultPPlus
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbLitho;
        private System.Windows.Forms.RadioButton rbIntegration;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label label1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnCalc = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbIntegration = new System.Windows.Forms.RadioButton();
            this.rbLitho = new System.Windows.Forms.RadioButton();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnCalc);
            this.panelTop.Controls.Add(this.groupBox1);
            this.panelTop.Controls.Add(this.btnBrowse);
            this.panelTop.Controls.Add(this.txtFile);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(984, 120);
            this.panelTop.TabIndex = 0;
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(680, 62);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(280, 40);
            this.btnCalc.TabIndex = 4;
            this.btnCalc.Text = "统计";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbIntegration);
            this.groupBox1.Controls.Add(this.rbLitho);
            this.groupBox1.Location = new System.Drawing.Point(80, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 52);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "类型选择";
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
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(830, 16);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(130, 28);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "选择 xlsx";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(80, 18);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(740, 21);
            this.txtFile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Excel：";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 120);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(984, 411);
            this.dgv.TabIndex = 1;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.lblStatus);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 531);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(984, 30);
            this.panelBottom.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblStatus.Size = new System.Drawing.Size(984, 30);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "就绪";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "MainForm";
            this.Text = "正确率统计工具";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

            // DataGridView 列（这里用代码加）
            this.dgv.Columns.Clear();
            this.dgv.Columns.Add("Type", "类型");
            this.dgv.Columns.Add("TrueCount", "TRUE");
            this.dgv.Columns.Add("FalseCount", "FALSE");
            this.dgv.Columns.Add("Total", "TOTAL");
            this.dgv.Columns.Add("SuccessRate", "成功率");
        }
    }
}
