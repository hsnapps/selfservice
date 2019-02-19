using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;

namespace SelfService.Code
{
    class Letter
    {
        PrintDocument document;

        public Letter() {
            TitleFont = new Font("Arial", 22);
            ContentFont = new Font("Arial", 16);
            SignatureFont = new Font("Arial", 20, FontStyle.Bold);
            HeaderFont = new Font("Arial", 16);
            ExtraFont = new Font("Arial", 16, FontStyle.Bold);

            document = new PrintDocument();
            PrinterSettings settings = new PrinterSettings {
                MaximumPage = 1,
                MinimumPage = 1,
                Copies = 1,
                ToPage = 1,
                FromPage = 1
            };
            IEnumerable<PaperSize> paperSizes = settings.PaperSizes.Cast<PaperSize>();
            PaperSize A4 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A4);
            document.DefaultPageSettings.PaperSize = A4;
            document.DefaultPageSettings.Margins = new Margins(100, 100, 100, 100);
        }

        public Font TitleFont { get; set; }
        public Font ContentFont { get; set; }
        public Font SignatureFont { get; set; }
        public Font HeaderFont { get; set; }
        public Font ExtraFont { get; set; }
        public PrintDocument PrintDocument { get { return document; } }
    }
}
