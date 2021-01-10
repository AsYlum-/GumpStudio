using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using GumpStudio.Forms;

namespace GumpStudio.Editors
{
    public class ClilocPropEditor : UITypeEditor
    {
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider == null)
            {
                return value;
            }

            var edSvc = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc == null)
            {
                return value;
            }

            ClilocBrowser clilocBrowser = new ClilocBrowser();
            return edSvc.ShowDialog(clilocBrowser) == DialogResult.OK ? clilocBrowser.ClilocId : value;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
