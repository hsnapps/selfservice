#define DIRECT
#undef DIRECT

using SelfService.Screens;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Media;
using SelfService.Code;

namespace SelfService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
#if DEBUG
            CultureInfo culture = new CultureInfo("en-SA");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture; 
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            var sound = Tools.LoadSound();
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = sound;
            player.PlayLooping();

#if DIRECT
            // open screen directly
            Models.Student student = DB.Execute.Login("438102728", "1103226302");
            BaseForm.Student = student; // new Models.Student("", "prog.hasan@gmail.com", "0569163852", "حسن علي باعبدالله", "Hassan A. Baabdullah", "1046328777", "بكالوريوس علوم الحاسب الآلي", "", "", "", "");
            Application.Run(new RestCourses());
#else
            Application.Run(new Start());
#endif
        }
    }
}
