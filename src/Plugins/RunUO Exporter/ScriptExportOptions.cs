using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace RunUOExporter
{
    public partial class ScriptExportOptions
    {
        private ClipNotice _clipNotice;
        private readonly ExportCs _co;
        private string _command = "";
        private bool _commandCall;

        public ScriptExportOptions(ExportCs imp)
        {
            InitializeComponent();
            Application.Idle += Application_Idle;
            _co = imp;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            IntStyle.Enabled = true;
            EnumStyle.Enabled = true;
        }

        private void ClipSaveNameClass_CheckedChanged(object sender, EventArgs e)
        {
            NameClassPanel.Enabled = ClipSaveNameClass.Checked;
            CheckEnums.Enabled = ClipSaveNameClass.Checked;
            if (ClipSaveNameClass.Checked)
            {
                ExtraOptions.Enabled = false;
                ExtraOptions.Checked = true;
                CheckEnums.Checked = true;
            }
            else
            {
                ExtraOptions.Enabled = true;
            }
        }

        private void ExportTypeRUCS_CheckedChanged(object sender, EventArgs e)
        {
            RunUOOptions.Enabled = ExportTypeRUCS.Checked;
        }

        private void EnumStyle_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                EnumStyle.Enabled = EnumStyle.Checked;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ExportOptions.cs EnumStyle_CheckChanged" + ex.Message);
            }
        }

        private void IntStyle_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                IntStyle.Enabled = IntStyle.Checked;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ExportOptions.cs IntStyle_CheckChanged" + ex.Message);
            }
        }

        private void ExtraOptions_CheckedChanged(object sender, EventArgs e)
        {
            OptionsGroup.Enabled = ExtraOptions.Checked;
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            bool intStyle = !EnumStyle.Checked && (IntStyle.Checked || true);
            string str;
            try
            {
                str = _co.Generate(intStyle, _command, _commandCall);
            }
            catch (Exception ex)
            {
                str = "Unknown Error";
                MessageBox.Show("ExportOptions.cs Generate_Click this.m_Co.Generate(style)" + ex.Message);
            }
            if (SaveTypeFile.Checked)
            {
                if (ExportTypeRUCS.Checked)
                {
                    SaveScriptAs.Filter = "C# File (*.cs)|*.cs";
                }
                else if (ExportTypeRUVB.Checked)
                {
                    SaveScriptAs.Filter = "VB.NET File (*.vb)|*.vb";
                }

                if (SaveScriptAs.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    StreamWriter streamWriter = new StreamWriter(SaveScriptAs.FileName);
                    streamWriter.Write(str);
                    streamWriter.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Unhandled Exception, StreamWriter writer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                if (_clipNotice == null)
                {
                    _clipNotice = new ClipNotice();
                }

                if (!_clipNotice.DontShow)
                {
                    _clipNotice.ShowDialog();
                }
                Clipboard.SetDataObject(str, true);
            }
        }

        private void SaveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void SaveTypeClip_CheckedChanged(object sender, EventArgs e)
        {
            NameClassPanel.Enabled = !SaveTypeClip.Checked;
            ClipboardPanel.Enabled = SaveTypeClip.Checked;
        }

        private void CommandCallTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!CommandCallCheck.Enabled)
            {
                return;
            }

            _command = CommandCallTextBox.Text;
        }

        private void CommandCallCheck_CheckedChanged(object sender, EventArgs e)
        {
            CommandCallCheck.Enabled = CommandCallCheck.Checked;
            _commandCall = true;
        }
    }
}
