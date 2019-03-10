#define DIRECT

using SelfService.Screens;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace SelfService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            CultureInfo culture = new CultureInfo("ar-SA");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

#if DIRECT
            // open screen directly
            Models.Student student = DB.Execute.Login("114361625", "1101449047");
            BaseForm.Student = student;
            Application.Run(new Plan());
#else
            Application.Run(new Form1());
#endif
        }
    }
}
