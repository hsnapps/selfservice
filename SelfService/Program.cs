#define DIRECT


using SelfService.Screens;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

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
            Models.Student student = DB.Execute.Login("114343617", "1087534176");
            BaseForm.Student = student; // new Models.Student("", "prog.hasan@gmail.com", "0569163852", "حسن علي باعبدالله", "Hassan A. Baabdullah", "1046328777", "بكالوريوس علوم الحاسب الآلي", "", "", "", "");
            Application.Run(new SelectLetters());
#else
            Application.Run(new Start());
#endif
        }
    }
}
