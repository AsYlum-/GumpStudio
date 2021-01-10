using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace GumpStudio.Forms
{
    public partial class AboutBoxForm
    {
        public AboutBoxForm()
        {
            InitializeComponent();
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutBoxForm_Load(object sender, EventArgs e)
        {
            lblVersion.Text = $"Core Version: {Assembly.GetExecutingAssembly().GetName().Version}";
        }

        private void LblHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = "http://www.orbsydia.net"
            });
        }

        public void SetText(string text)
        {
            txtAbout.Text = $"Gump Studio was designed and written by Bradley Uffner in 2004. It makes extensive use of a modified version of the UOSDK written by Krrios, available at www.RunUO.com. Artwork was created by Melanius, and several more ideas were contributed by the RunUO community.  Special thanks go to DarkStorm of the Wolfpack emulator for helping me to decode unifont.mul, allowing me to displaying UO fonts correctly.\r\n\r\n====Plugin Specific Information====\r\n{text}";
        }
    }
}
