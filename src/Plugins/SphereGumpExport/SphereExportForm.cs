using System;
using System.IO;
using System.Windows.Forms;

namespace SphereGumpExport
{
    public partial class SphereExportForm
    {
        private readonly SphereExporter _seWorker;

        public string GumpName => txt_Gumpname.Text;

        public SphereExportForm(SphereExporter seJob)
        {
            InitializeComponent();
            _seWorker = seJob;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Sphere Script (*.scp)|*.scp"
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            txt_Savefile.Text = saveFileDialog.FileName;
        }

        private void Btn_Export_Click(object sender, EventArgs e)
        {
            if (txt_Savefile.Text.Length == 0)
            {
                MessageBox.Show("Please Specify the File Name.");
            }
            else if (txt_Gumpname.Text.Length == 0)
            {
                MessageBox.Show("Please Specify the Gump Name.");
            }
            else
            {
                StreamWriter text = File.CreateText(txt_Savefile.Text);
                StringWriter sphereScript = _seWorker.GetSphereScript(rbt_Sphere56.Checked);
                text.Write(sphereScript.ToString());
                sphereScript.Close();
                text.Close();
                Close();
            }
        }
    }
}
