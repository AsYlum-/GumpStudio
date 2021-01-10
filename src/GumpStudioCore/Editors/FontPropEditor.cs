using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using GumpStudio.UserControls;

namespace GumpStudio.Editors
{
    public class FontPropEditor : UITypeEditor
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
            if (value != null && value.GetType() != typeof(int))
            {
                return value;
            }

            ReturnValue = Convert.ToInt32(value);
            if (provider != null)
            {
                EdSvc = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
            }

            if (EdSvc == null)
            {
                return value;
            }

            FontBrowser fontBrowser = new FontBrowser(Convert.ToInt32(value));
            fontBrowser.ValueChanged += ValueSelected;
            EdSvc.DropDownControl(fontBrowser);
            fontBrowser.Dispose();
            return ReturnValue;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return false;
        }

        protected void ValueSelected(int value)
        {
            EdSvc.CloseDropDown();
            ReturnValue = value;
        }
    }
}
