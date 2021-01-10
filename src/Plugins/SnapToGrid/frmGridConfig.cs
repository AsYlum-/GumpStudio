using System;

namespace SnapToGrid
{
    public partial class FrmGridConfig
    {
        public GridConfiguration Config;

        public FrmGridConfig()
        {
            InitializeComponent();
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void CmdOK_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtWidth.Text, out int outWidth) && int.TryParse(txtHeight.Text, out int outHeight))
            {
                if (outWidth >= 1 && outHeight >= 1)
                {
                    Config = new GridConfiguration(new System.Drawing.Size(outWidth, outHeight),
                                                   lblGridColor.BackColor,
                                                   Config.ShowGrid);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    DialogResult = System.Windows.Forms.MessageBox.Show("Invalid Width and Height.");
                }
            }
            else
            {
                DialogResult = System.Windows.Forms.MessageBox.Show("Invalid Width and Height.");
            }
        }

        private void FrmGridConfig_Load(object sender, EventArgs e)
        {
            txtWidth.Text = Config.GridSize.Width.ToString();
            txtHeight.Text = Config.GridSize.Height.ToString();
            lblGridColor.BackColor = Config.GridColor;
        }

        private void LblGridColor_Click(object sender, EventArgs e)
        {
            ColorPicker.Color = lblGridColor.BackColor;
            if (ColorPicker.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            lblGridColor.BackColor = ColorPicker.Color;
        }
    }
}
