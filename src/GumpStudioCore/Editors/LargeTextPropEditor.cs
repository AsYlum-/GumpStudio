using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using GumpStudio.Forms;

namespace GumpStudio.Editors
{
    public class LargeTextPropEditor : UITypeEditor
    {
        protected IWindowsFormsEditorService EdSvc;

        protected int ReturnValue;

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            EdSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (EdSvc == null)
            {
                return value;
            }

            LargeTextEditor largeTextEditor = new LargeTextEditor
            {
                TxtText = { Text = Convert.ToString(value) }
            };

            return EdSvc.ShowDialog(largeTextEditor) == DialogResult.OK ? largeTextEditor.TxtText.Text : value;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
