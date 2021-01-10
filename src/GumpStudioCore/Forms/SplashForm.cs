using System;
using System.Threading;
using System.Windows.Forms;

namespace GumpStudio.Forms
{
    public sealed partial class SplashForm
    {
        private static SplashForm _splashForm;
        private static Thread _thread;

        private SplashForm()
        {
            InitializeComponent();
        }

        public static void DisplaySplash()
        {
            _thread = new Thread(ThreadStartDisplay);
            _thread.Start();
        }

        private static void FadeOut(IDisposable f)
        {
            f.Dispose();
        }

        private void SplashForm_Click(object sender, EventArgs e)
        {
            FadeOut(this);
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            CenterToScreen();
        }

        private static void ThreadStartDisplay()
        {
            _splashForm = new SplashForm();
            _splashForm.Show();

            DateTime now = DateTime.Now;
            while (DateTime.Compare(DateTime.Now, now.AddSeconds(2.0)) <= 0)
            {
                Thread.Sleep(100);
                Application.DoEvents();
            }

            FadeOut(_splashForm);
        }
    }
}
