using System;
using System.Collections;
using System.Windows.Forms;

namespace ImportRunUOPlugin
{
    public partial class FrmInterpret
    {
        public FrmInterpret()
        {
            InitializeComponent();
        }

        private void FillLines(ArrayList lines, long selLine)
        {
            txtLines.Text = "";
            long num = 0;
            foreach (object value in lines)
            {
                string text = Convert.ToString(value);
                num++;
                txtLines.Text = string.Concat(txtLines.Text, num.ToString(), ":\t", text, "\r\n");

                if (num == selLine)
                {
                    txtLines.Select(txtLines.Text.Length - text.Length, text.Length);
                }
            }
        }

        public string Interpret(string text, long line, ArrayList lines)
        {
            FillLines(lines, line);
            txtNewValue.Text = text;
            lblLine.Text = line.ToString();

            return ShowDialog() == DialogResult.OK ? txtNewValue.Text : "0";
        }

        private void CmdOK_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtNewValue.Text, out _))
            {
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
