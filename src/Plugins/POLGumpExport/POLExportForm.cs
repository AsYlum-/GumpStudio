using System;
using System.IO;
using System.Windows.Forms;

namespace POLGumpExport
{
    public partial class PolExportForm
    {
        public string GumpName => txt_Gumpname.Text;

        private readonly PolExporter _seWorker;

        public PolExportForm(PolExporter seJob)
        {
            InitializeComponent();
            _seWorker = seJob;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "POL Script (*.src)|*.src"
            };

            if (txt_Gumpname.Text != null)
            {
                saveFileDialog.FileName = txt_Gumpname.Text;
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                txt_Savefile.Text = saveFileDialog.FileName;
            }
        }

        private void Export_Click(object sender, EventArgs e)
        {
            if (txt_Savefile.Text.Length == 0)
            {
                MessageBox.Show("Please Specify the File Name.");
                return;
            }

            if (txt_Gumpname.Text.Length == 0)
            {
                MessageBox.Show("Please Specify the Gump Name.");
                return;
            }

            using (var streamWriter = File.CreateText(txt_Savefile.Text))
            {
                using (var polScript = _seWorker.GetPolScript(rbt_Distro.Checked, chk_comments.Checked,
                    chk_names.Checked, chk_DefaultTexts.Checked))
                {
                    streamWriter.Write(polScript.ToString());
                }
            }

            Close();
        }

        private void Distro_CheckedChanged(object sender, EventArgs e)
        {
            if (rbt_Distro.Checked)
            {
                chk_names.Enabled = true;
                chk_comments.Enabled = true;
                chk_DefaultTexts.Enabled = true;
                return;
            }

            chk_names.Enabled = false;
            chk_DefaultTexts.Enabled = false;
            chk_comments.Enabled = false;
        }
    }
}