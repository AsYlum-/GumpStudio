using System;
using System.Windows.Forms;

namespace GumpStudio.Forms
{
    public partial class Settings
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
