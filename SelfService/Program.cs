using SelfService.Documents;
using SelfService.Screens;
using System;
using System.Windows.Forms;

namespace SelfService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            //Application.Run(new Start());

            Models.Student student = DB.Execute.Login("114361625", "1101449047");
            BaseForm.Student = student;
            Application.Run(new LetterPrint(new ExaminationCertificateLetter("1440-06-01", "1440-06-15")));
        }
    }
}
