using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GumpStudio.Classes;
using Ultima;

namespace GumpStudio.Forms
{
    public partial class ClilocBrowser
    {
        private ListBox _clilocCache;

        public int ClilocId { get; set; }

        public ClilocBrowser()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ClilocBrowser_Load(object sender, EventArgs e)
        {
            foreach (string file in Directory.GetFiles(AppSettings.Default.ClientPath, "Cliloc.*"))
            {
                cboLanguage.Items.Add(Path.GetExtension(file).Substring(1));
            }

            if (_clilocCache == null || _clilocCache.Items.Count == 0)
            {
                lstCliloc.SuspendLayout();
                lstCliloc.BeginUpdate();
                foreach (StringEntry entry in new StringList("enu").Entries)
                {
                    lstCliloc.Items.Add(entry);
                }
                lstCliloc.EndUpdate();
                lstCliloc.ResumeLayout();
                _clilocCache = lstCliloc;
            }
            else
            {
                lstCliloc.SuspendLayout();
                lstCliloc.BeginUpdate();
                lstCliloc.Items.AddRange(_clilocCache.Items);
                lstCliloc.EndUpdate();
                lstCliloc.ResumeLayout();
            }
        }

        private void LstCliloc_DrawItem(object sender, DrawItemEventArgs e)
        {
            // TODO: fix drawItem - crashes on subsequent attempts?
            StringEntry stringEntry = (StringEntry)lstCliloc.Items[e.Index];

            e.DrawBackground();
            e.Graphics.DrawString(stringEntry.Number.ToString(), lstCliloc.Font, Brushes.Black, e.Bounds.X, e.Bounds.Top);
            e.Graphics.DrawString(stringEntry.Text, lstCliloc.Font, Brushes.Black, e.Bounds.X + 100.0f, e.Bounds.Top);
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void LstCliloc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: fix setting cliloc id 
            StringEntry selectedCliloc = (StringEntry)_clilocCache.SelectedItem;
            if (selectedCliloc != null)
            {
                ClilocId = selectedCliloc.Number;
            }
        }
    }
}
