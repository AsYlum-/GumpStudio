using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WallPaperPlugin
{
    public partial class FrmConfig
    {
        public Color BackGroundColor;
        public string ImagePath;

        internal virtual CheckBox ChkImage
        {
            get => chkImage;
            set => chkImage = value;
        }

        public FrmConfig()
        {
            InitializeComponent();
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void CmdOK_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtImagePath.Text) || !chkImage.Checked)
            {
                ImagePath = txtImagePath.Text;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Image path does not exist");
            }
        }

        private void LblBGColor_Click(object sender, EventArgs e)
        {
            ColorPicker.AllowFullOpen = true;
            ColorPicker.AnyColor = true;
            ColorPicker.Color = lblBGColor.BackColor;

            if (ColorPicker.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            BackGroundColor = ColorPicker.Color;
            lblBGColor.BackColor = BackGroundColor;
        }

        private void CmdBrowse_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            txtImagePath.Text = OpenFileDialog.FileName;
            ImagePath = OpenFileDialog.FileName;
            chkImage.Checked = true;
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            txtImagePath.Text = ImagePath;
            lblBGColor.BackColor = BackGroundColor;
        }
    }
}
