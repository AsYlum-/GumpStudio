using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using GumpStudio.Forms;
using Ultima;

namespace GumpStudio.Editors
{
    public class GumpIdPropEditor : UITypeEditor
    {
        protected IWindowsFormsEditorService EdSvc;
        protected int ReturnValue;

        /*
                protected static Color Convert555ToARGB(short Col)
                {
                    return Color.FromArgb(((short)(Col >> 10) & 0x1F) * 8, ((short)(Col >> 5) & 0x1F) * 8, (Col & 0x1F) * 8);
                }
        */

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            EdSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (EdSvc == null)
            {
                return value;
            }

            using (GumpArtBrowser gumpArtBrowser = new GumpArtBrowser {GumpId = Convert.ToInt32(value)})
            {
                if (EdSvc.ShowDialog(gumpArtBrowser) == DialogResult.OK)
                {
                    Image gump = Gumps.GetGump(gumpArtBrowser.GumpId);
                    if (gump != null)
                    {
                        gump.Dispose();
                        ReturnValue = gumpArtBrowser.GumpId;
                        gumpArtBrowser.Dispose();
                        return ReturnValue;
                    }

                    MessageBox.Show("invalid GumpID");

                    return value;
                }
            }

            return value;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
