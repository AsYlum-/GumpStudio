using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using GumpStudio.UserControls;
using Ultima;

namespace GumpStudio.Editors
{
    public class HuePropEditor : UITypeEditor
    {
        protected IWindowsFormsEditorService EdSvc;

        protected Hue ReturnValue;

        protected static Color Convert555ToArgb(short color)
        {
            return Color.FromArgb(((short)(color >> 10) & 0x1F) * 8, ((short)(color >> 5) & 0x1F) * 8, (color & 0x1F) * 8);
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (value == null)
            {
                value = Hues.GetHue(0);
            }

            if (value.GetType() != typeof(Hue))
            {
                return value;
            }

            EdSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (EdSvc == null)
            {
                return value;
            }

            HuePickerControl huePickerControl = new HuePickerControl((Hue)value);
            huePickerControl.ValueChanged += ValueSelected;
            EdSvc.DropDownControl(huePickerControl);
            if (ReturnValue != null)
            {
                huePickerControl.Dispose();
                return ReturnValue;
            }
            huePickerControl.Dispose();
            return value;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public override void PaintValue(PaintValueEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.FillRectangle(Brushes.White, e.Bounds);
            float num = (float)(e.Bounds.Width - 3) / 32f;
            Hue hue = (Hue)e.Value;
            if (hue != null)
            {
                int num2 = 0;
                foreach (short col in hue.Colors)
                {
                    int x = (int)Math.Round((double)e.Bounds.X + (double)num2 * (double)num);
                    int y = e.Bounds.Y;
                    int width = (int)Math.Round(num) + 1;
                    int height = e.Bounds.Height;
                    graphics.FillRectangle(new SolidBrush(Convert555ToArgb(col)), new Rectangle(x, y, width, height));
                    num2++;
                }
            }
        }

        protected void ValueSelected(Hue hue)
        {
            EdSvc.CloseDropDown();
            ReturnValue = hue;
        }
    }
}
