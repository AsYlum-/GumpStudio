using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using GumpStudio.Forms;
using Ultima;

namespace GumpStudio.Editors
{
    public class ItemIdPropEditor : UITypeEditor
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

            NewStaticArtBrowser newStaticArtBrowser = new NewStaticArtBrowser
            {
                ItemId = Convert.ToInt32(value)
            };

            if (EdSvc.ShowDialog(newStaticArtBrowser) != DialogResult.OK)
            {
                return value;
            }

            if (Art.GetStatic(newStaticArtBrowser.ItemId) != null)
            {
                ReturnValue = newStaticArtBrowser.ItemId;
                newStaticArtBrowser.Dispose();
                return ReturnValue;
            }

            MessageBox.Show("invalid ItemID");

            return value;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
