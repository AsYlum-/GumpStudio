using System;
using System.Drawing;
using System.Windows.Forms;
using GumpStudio.Classes;
using GumpStudio.Elements;
using UOFont;

namespace GumpStudio.UserControls
{
    public partial class FontBrowser
    {
        private int _value;
        private bool _fontUnicode = true;

        public delegate void ValueChangedEventHandler(int value);
        public event ValueChangedEventHandler ValueChanged;

        public FontBrowser()
        {
            InitializeComponent();
        }

        public FontBrowser(int value) : this()
        {
            _value = value;
        }

        private void FontBrowser_Load(object sender, EventArgs e)
        {
            _fontUnicode = true;
            var selectedElements = GlobalObjects.DesignerForm?.ElementStack != null
                    ? GlobalObjects.DesignerForm.ElementStack.GetSelectedElements()
                    : null;

            if (selectedElements != null)
            {
                foreach (BaseElement element in selectedElements)
                {
                    if (element is LabelElement labelElement && !labelElement.Unicode)
                    {
                        _fontUnicode = false;
                        break;
                    }
                }
            }

            for (int i = 0; i < (_fontUnicode ? 13 : 10); i++)
            {
                if (i >= 0)
                {
                    lstFont.Items.Add(i);
                }
            }

            lstFont.SelectedIndex = _value;
        }

        private void LstFont_DoubleClick(object sender, EventArgs e)
        {
            _value = lstFont.SelectedIndex;
            //_value = lstFont.SelectedIndex + 1; // older version 
            ValueChangedEventHandler valueChanged = ValueChanged;

            valueChanged?.Invoke(_value);
        }

        private void LstFont_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.FillRectangle(
                (e.State & DrawItemState.Selected) > DrawItemState.None
                    ? SystemBrushes.Highlight
                    : SystemBrushes.Window, e.Bounds);

            if (e.Index > (_fontUnicode ? 12 : 9))
            {
                return;
            }

            using (Bitmap bitmap = _fontUnicode
                ? UnicodeFonts.GetStringImage(e.Index, "ABCabc123!@#$АБВабв")
                : Fonts.GetStringImage(e.Index, "ABCabc123 */ АБВабв"))
            {
                e.Graphics.DrawImage(bitmap, e.Bounds.Location);
            }
        }

        private void LstFont_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index > (_fontUnicode ? 12 : 9))
            {
                e.ItemHeight = 0;
                return;
            }

            using (Bitmap bitmap = _fontUnicode
                ? UnicodeFonts.GetStringImage(e.Index, "ABCabc123!@#$АБВабв")
                : Fonts.GetStringImage(e.Index, "ABCabc123 */ АБВабв"))
            {
                e.ItemHeight = bitmap.Height;
            }
        }
    }
}
