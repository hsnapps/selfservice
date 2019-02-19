using SelfService.Code;
using SelfService.Properties;
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
