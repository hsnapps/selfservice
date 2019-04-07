using SelfService.Code;
using SelfService.Models;
using SelfService.Properties;
using SelfService.Screens;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;

namespace SelfService.Documents
{
    class LetterOfCertificate : PrintDocument
    {
        readonly Image logo;
        readonly Student student;
        readonly string to;

        public LetterOfCertificate(string to) {
            logo = Tools.LoadImage("Logo.png");
            student = BaseForm.Student;
            this.to = to;
        }

        protected override void OnPrintPage(PrintPageEventArgs e) {
            var academic = DB.Execute.GetValues("academic");
            var academicYear = academic["currentYear"];
            var years = academicYear.Split('/');
            //var nextYear = academic["nextYear"];
            var currentTerm = academic["currentTerm"];
            string managerTitle = "", managerName = "";
            StringFormat near = new StringFormat {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.DirectionRightToLeft,
            };
            StringFormat center = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.DirectionRightToLeft,
            };
            Rectangle headerRectangle = new Rectangle(500, 135, 250, 70);
            Rectangle logoRectangle = new Rectangle(324, 88, 120, 70);
            Rectangle titleRectangle = new Rectangle(0, 220, Tools.PaperWidth, 43);
            Rectangle bodyRectangle = new Rectangle(50, 340, Tools.PaperWidth - 90, 377);
            DB.Execute.GetManager(ref managerTitle, ref managerName);

            string body = Resources.LetterOfCertificate
                .Replace("name", student.Name_AR)
                .Replace("id", student.ID)
                .Replace("program", student.Section)
                .Replace("major", student.Program)
                .Replace("year1", years[0])
                .Replace("year2", years[1])
                .Replace("term", currentTerm)
                .Replace("managerTitle", managerTitle)
                .Replace("managerName", managerName)
                .Replace("to", to);
            string header = Resources.SaudiCouncilOfEngineersHeader.Replace("term", currentTerm);

            using (Font font = new Font("Arial", 11.5f, FontStyle.Bold)) {
                e.Graphics.DrawString(Tools.ToHindi(header), font, Brushes.Black, headerRectangle, near);
                e.Graphics.DrawImage(logo, logoRectangle);
                e.Graphics.DrawString(Tools.ToHindi(body), font, Brushes.Black, bodyRectangle, near);
            }
            using (Font font = new Font("Arial", 28.0f, FontStyle.Bold)) {
                e.Graphics.DrawString(Tools.ToHindi(Resources.SaudiCouncilOfEngineersTitle), font, Brushes.Black, titleRectangle, center);
            }
        }
    }
}
