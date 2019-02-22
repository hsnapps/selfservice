using SelfService.Code;
using SelfService.Properties;
using SelfService.Screens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SelfService.Documents
{
    class ExaminationCertificateLetter : PrintDocument
    {
        Bitmap logo;

        public ExaminationCertificateLetter(string start, string end) {
            Start = start;
            End = end;
            LoadImages();

            PrinterSettings ps = new PrinterSettings();
            IEnumerable<PaperSize> paperSizes = ps.PaperSizes.Cast<PaperSize>();
            // setting paper size to A4 size
            PaperSize A4 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A4);
            this.DefaultPageSettings.PaperSize = A4;
            //this.DefaultPageSettings.Margins = new Margins(55, 59, 197, 60);
            this.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
        }

        void LoadImages() {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("SelfService.Images.Logo.png")) {
                logo = new Bitmap(stream);
                stream.Close();
            }
        }

        protected override void OnPrintPage(PrintPageEventArgs e) {
            PrintHeader(e.Graphics);
            PrintTable(e.Graphics);
            PrintBody(e.Graphics);
        }

        void PrintBody(Graphics g) {
            Rectangle rect = new Rectangle {
                X = 55,
                Y = 550,
                Width = 715,
                Height = 175,
            };

            string startYear = Start.Substring(0, 4);
            string startMonth = Start.Substring(5, 2);
            string startDay = Start.Substring(8, 2);
            string start = startDay + " / " + startMonth + " / " + startYear;
            string endYear = End.Substring(0, 4);
            string endMonth = End.Substring(5, 2);
            string endDay = End.Substring(8, 2);
            string end = endDay + " / " + endMonth + " / " + endYear;

            string year2 = End.Substring(0, 4);
            string year1 = (Int32.Parse(year2) - 1).ToString();
            string body = Resources.ExamBody
                .Replace("start", start)
                .Replace("end", end)
                .Replace("year1", year1)
                .Replace("year2", year2);
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.DirectionRightToLeft,
                Trimming = StringTrimming.Word,
            };

            using (Font font = new Font(Fonts.ALMohanad, 15)) {
                g.DrawString(body, font, Brushes.Black, rect, format);
            }
        }

        void PrintTable(Graphics g) {
            int x = 53;
            int y = 424;
            int height = 111;
            int titleWidth = 130;
            int line1 = 463;
            int line2 = 499;
            Color grey = Color.FromArgb(242, 242, 242);
            Rectangle outer = new Rectangle(x, y, 719, height);
            Rectangle first = new Rectangle(645, y, titleWidth, height);
            Rectangle third = new Rectangle(280, y, titleWidth, height);

            using (Pen pen = new Pen(Color.Black, 3)) {
                g.DrawRectangle(pen, outer);
                using (Brush brush = new SolidBrush(grey)) {
                    g.FillRectangle(brush, first);
                    g.FillRectangle(brush, third);
                }
                g.DrawRectangle(pen, first);
                g.DrawRectangle(pen, third);
                g.DrawLine(pen, new Point(x, line1), new Point(outer.Right + 2, line1));
                g.DrawLine(pen, new Point(x, line2), new Point(outer.Right + 2, line2));
            }

            int lineHeight = line1 - y;
            int valueWidth = first.Left - third.Right;

            Rectangle[] titlesRects = new Rectangle[6];
            for (int i = 0; i < titlesRects.Length / 2; i++) {
                titlesRects[i] = new Rectangle(first.X, y + (lineHeight * i), titleWidth, lineHeight);
            }
            for (int i = 3; i < titlesRects.Length; i++) {
                titlesRects[i] = new Rectangle(third.X, y + (lineHeight * (i - 3)), titleWidth, lineHeight);
            }

            Rectangle[] valuesRects = new Rectangle[6];
            for (int i = 0; i < valuesRects.Length / 2; i++) {
                valuesRects[i] = new Rectangle(third.Right, y + (lineHeight * i), valueWidth, lineHeight);
            }
            for (int i = 3; i < valuesRects.Length; i++) {
                valuesRects[i] = new Rectangle(x, y + (lineHeight * (i - 3)), valueWidth, lineHeight);
            }
            string major = String.Format("{0} - {1}", BaseForm.Student.Level, BaseForm.Student.Program);
            string[] titlesStrings = new string[] {
                Resources.ID,
                Resources.Section,
                Resources.ExamStart,
                Resources.TraineeName,
                Resources.Major,
                Resources.ExamEnd
            }; ;
            string[] valuesStrings = new string[] {
                BaseForm.Student.ID,
                major,
                End,
                BaseForm.Student.Name_AR,
                BaseForm.Student.Section,
                Start
            };
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.DirectionRightToLeft,
            };

            using (Font numaric = new Font(Fonts.ALMohanadBold, 12)) {
                using (Font mohanad = new Font(Fonts.ALMohanadBold, 12)) {
                    for (int i = 0; i < valuesRects.Length; i++) {
                        if (i == 0 || i == 2 || i == 5) {
                            g.DrawString(valuesStrings[i], numaric, Brushes.Black, valuesRects[i], format);
                        } else {
                            g.DrawString(valuesStrings[i], mohanad, Brushes.Black, valuesRects[i], format);
                        }
                    }
                }
            }

            using (Font font = new Font(Fonts.ALMohanadBold, 12)) {
                for (int i = 0; i < titlesRects.Length; i++) {
                    g.DrawString(titlesStrings[i], font, Brushes.Black, titlesRects[i], format);
                }
            }
        }

        void PrintHeader(Graphics g) {
            float height = 133;
            float y = 50;
            RectangleF leftRect = new RectangleF(20, y, 350, height);
            RectangleF middleRect = new RectangleF(560, y, 283, height);
            RectangleF rightRect = new RectangleF(540, y, 228, height);
            StringFormat rightFormat = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.DirectionRightToLeft | StringFormatFlags.NoFontFallback
            };
            StringFormat leftFormat = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
            };
            StringFormat centerRtl = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
            };
            Font engFont_10 = new Font(Fonts.TimesNewRoman, 9f);
            Font engFont_8 = new Font(Fonts.TimesNewRoman, 8f);
            Font arbFont_m = new Font(Fonts.ALMohanadBold, 10f);
            Font arbFont_h = new Font(Fonts.HeshamAlSharq, 12f);
            Font titleFont = new Font(Fonts.HeshamAlSharq, 38.0f);

            g.DrawString(Resources.Kingdom_En, engFont_10, Brushes.Black, new RectangleF(102, 55, 203, 17), leftFormat);
            g.DrawString(Resources.TVTC_En_Full, engFont_8, Brushes.Black, new RectangleF(59, 96, 288, 17), leftFormat);
            g.DrawString(Resources.College_En, engFont_10, Brushes.Black, new RectangleF(56, 128, 295, 17), leftFormat);
            g.DrawString(Resources.Admission_En, engFont_10, Brushes.Black, new RectangleF(63, 161, 280, 17), leftFormat);

            g.DrawImage(logo, new PointF(355, 67));

            g.DrawString(Resources.Kingdom_Ar, arbFont_m, Brushes.Black, new RectangleF(578, 61, 156, 21), leftFormat);
            g.DrawString(Resources.TVTC_Ar_Full, arbFont_h, Brushes.Black, new RectangleF(545, 94, 230, 23), leftFormat);
            g.DrawString(Resources.College_Ar, arbFont_m, Brushes.Black, new RectangleF(550, 127, 211, 19), leftFormat);
            g.DrawString(Resources.Admission_Ar, arbFont_m, Brushes.Black, new RectangleF(563, 161, 187, 19), leftFormat);

            g.DrawString(Resources.Ifadah, titleFont, Brushes.Black, new RectangleF(0, 227, 827, 164), rightFormat);
        }

        public string Start { get; private set; }
        public string End { get; private set; }
    }
}
