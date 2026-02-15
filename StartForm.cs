using System;
using System.Windows.Forms;

namespace ResultPPlus
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void btnSingle_Click(object sender, EventArgs e)
        {
            using (var form = new MainForm())
            {
                form.ShowDialog(this);
            }
        }

        private void btnBatch_Click(object sender, EventArgs e)
        {
            using (var form = new BatchForm())
            {
                form.ShowDialog(this);
            }
        }

        private void btnWriteBack_Click(object sender, EventArgs e)
        {
            using (var form = new ErrorWriteBackForm())
            {
                form.ShowDialog(this);
            }
        }
    }
}
