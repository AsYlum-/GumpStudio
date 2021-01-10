using System;
using System.Windows.Forms;

namespace GumpStudio.Forms
{
    public partial class LargeTextEditor
    {
        public LargeTextEditor()
        {
            InitializeComponent();
        }

        // TODO: check is this prop working as it should.
        // ReSharper disable once ConvertToAutoProperty
        public virtual TextBox TxtText
        {
            get => txtText;
            set => txtText = value;
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void CmdOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
