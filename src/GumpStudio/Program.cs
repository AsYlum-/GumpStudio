using System;
using System.Windows.Forms;
using GumpStudio.Classes;
using GumpStudio.Forms;

namespace GumpStudio
{
    internal static class Program
    {
        [STAThread]
        public static void Main()
        {
            AppDomain.CurrentDomain.SetupInformation.PrivateBinPath = "Plugins";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DesignerForm designerForm = new DesignerForm();
            GlobalObjects.DesignerForm = designerForm;
            Application.Run(designerForm);
        }
    }
}
