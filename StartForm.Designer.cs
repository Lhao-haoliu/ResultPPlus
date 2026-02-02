namespace ResultPPlus
{
    partial class StartForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnSingle;
        private System.Windows.Forms.Button btnBatch;
        private System.Windows.Forms.Button btnWriteBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.btnBatch = new System.Windows.Forms.Button();
            this.btnWriteBack = new System.Windows.Forms.Button();
            this.btnSingle = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.btnBatch);
            this.panelMain.Controls.Add(this.btnSingle);
            this.panelMain.Controls.Add(this.btnWriteBack);
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(520, 300);
            this.panelMain.TabIndex = 0;
            // 
            // btnBatch
            // 
            this.btnBatch.Location = new System.Drawing.Point(280, 140);
            this.btnBatch.Name = "btnBatch";
            this.btnBatch.Size = new System.Drawing.Size(160, 50);
            this.btnBatch.TabIndex = 2;
            this.btnBatch.Text = "批量统计";
            this.btnBatch.UseVisualStyleBackColor = true;
            this.btnBatch.Click += new System.EventHandler(this.btnBatch_Click);
            // 
            // btnWriteBack
            // 
            this.btnWriteBack.Location = new System.Drawing.Point(180, 200);
            this.btnWriteBack.Name = "btnWriteBack";
            this.btnWriteBack.Size = new System.Drawing.Size(160, 40);
            this.btnWriteBack.TabIndex = 3;
            this.btnWriteBack.Text = "异常回写";
            this.btnWriteBack.UseVisualStyleBackColor = true;
            this.btnWriteBack.Click += new System.EventHandler(this.btnWriteBack_Click);
            // 
            // btnSingle
            // 
            this.btnSingle.Location = new System.Drawing.Point(80, 140);
            this.btnSingle.Name = "btnSingle";
            this.btnSingle.Size = new System.Drawing.Size(160, 50);
            this.btnSingle.TabIndex = 1;
            this.btnSingle.Text = "单个统计";
            this.btnSingle.UseVisualStyleBackColor = true;
            this.btnSingle.Click += new System.EventHandler(this.btnSingle_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(520, 80);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "请选择统计模式";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // StartForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 300);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartForm";
            this.Text = "正确率统计工具";
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
