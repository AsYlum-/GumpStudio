using System;
using System.Drawing;
using System.Windows.Forms;
using Ultima;

namespace GumpStudio.UserControls
{
    public partial class HuePickerControl
    {
        private Hue _selectedHue;

        public event ValueChangedEventHandler ValueChanged;

        public delegate void ValueChangedEventHandler(Hue hue);

        public HuePickerControl()
        {
            InitializeComponent();
        }

        public HuePickerControl(Hue initialHue) : this()
        {
            _selectedHue = initialHue;
        }

        private void CboQuick_SelectedIndexChanged(object sender, EventArgs e)
        {
            int groupIndex = 0;
            switch (cboQuick.Text)
            {
                case "Colors":
                    groupIndex = 0;
                    break;
                case "Skin":
                    groupIndex = 1001;
                    break;
                case "Hair":
                    groupIndex = 1101;
                    break;
                case "Interesting #1":
                    groupIndex = 1049;
                    break;
                case "Pinks":
                    groupIndex = 1200;
                    break;
                case "Elemental Weapons":
                    groupIndex = 1254;
                    break;
                case "Interesting #2":
                    groupIndex = 1278;
                    break;
                case "Blues":
                    groupIndex = 1300;
                    break;
                case "Elemental Wear":
                    groupIndex = 1354;
                    break;
                case "Greens":
                    groupIndex = 1400;
                    break;
                case "Oranges":
                    groupIndex = 1500;
                    break;
                case "Reds":
                    groupIndex = 1600;
                    break;
                case "Yellows":
                    groupIndex = 1700;
                    break;
                case "Neutrals":
                    groupIndex = 1800;
                    break;
                case "Snakes":
                    groupIndex = 2000;
                    break;
                case "Birds":
                    groupIndex = 2100;
                    break;
                case "Slimes":
                    groupIndex = 2200;
                    break;
                case "Animals":
                    groupIndex = 2300;
                    break;
                case "Metals":
                    groupIndex = 2400;
                    break;
            }

            // TODO: we need better way to search for hue index - maybe some customizable list
            lstHue.SelectedIndex = groupIndex;
            lstHue.Focus();
        }

        private static Color Convert555ToArgb(short col)
        {
            return Color.FromArgb(((short)(col >> 10) & 31) * 8, ((short)(col >> 5) & 31) * 8, (col & 31) * 8);
        }

        private void HuePickerControl_Load(object sender, EventArgs e)
        {
            lstHue.Items.Clear();
            foreach (Hue hue in Hues.List)
            {
                if (hue.Index == _selectedHue.Index)
                {
                    lstHue.SelectedIndex = lstHue.Items.Add(hue);
                }
                else
                {
                    lstHue.Items.Add(hue);
                }
            }
            StatusBar.Text = $"{_selectedHue.Index}: {_selectedHue.Name}";
        }

        private void LstHue_DoubleClick(object sender, EventArgs e)
        {
            ValueChangedEventHandler valueChanged = ValueChanged;

            valueChanged?.Invoke(_selectedHue);
        }

        private void LstHue_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            Hue hue = (Hue)lstHue.Items[e.Index];

            DrawHueLabel(e, hue);
            DrawHueColors(e, hue);

            bool isItemSelected = (e.State & DrawItemState.Selected) > DrawItemState.None;
            if (isItemSelected)
            {
                e.DrawFocusRectangle();
            }
        }

        private static void DrawHueLabel(DrawItemEventArgs e, Hue hue)
        {
            e.Graphics.DrawString(hue.Index.ToString(), e.Font, Brushes.Black, e.Bounds.X + 3, e.Bounds.Y);
        }

        private static void DrawHueColors(DrawItemEventArgs e, Hue hue)
        {
            float columnOffset = (e.Bounds.Width - 35) / 32f;
            int column = 0;
            foreach (short color in hue.Colors)
            {
                int x = e.Bounds.X + 35 + (int)Math.Round(column * (double)columnOffset);
                int y = e.Bounds.Y;

                int width = (int)Math.Round(columnOffset + 1.0);
                int height = e.Bounds.Height;

                e.Graphics.FillRectangle(new SolidBrush(Convert555ToArgb(color)), new Rectangle(x, y, width, height));

                ++column;
            }
        }

        private void LstHue_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedHue = lstHue.SelectedItem as Hue;
            if (_selectedHue == null)
            {
                return;
            }

            StatusBar.Text = $"{_selectedHue.Index}: {_selectedHue.Name}";
        }
    }
}
