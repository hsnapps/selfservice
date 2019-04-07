using SelfService.Components;
using SelfService.Properties;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace SelfService.Code
{
    static class Tools
    {
        internal static string ToHindi(this string input) {
            char[] arabic = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] hindi = new char[] { '٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩' };

            return input.Replace(arabic[0], hindi[0])
                        .Replace(arabic[1], hindi[1])
                        .Replace(arabic[2], hindi[2])
                        .Replace(arabic[3], hindi[3])
                        .Replace(arabic[4], hindi[4])
                        .Replace(arabic[5], hindi[5])
                        .Replace(arabic[6], hindi[6])
                        .Replace(arabic[7], hindi[7])
                        .Replace(arabic[8], hindi[8])
                        .Replace(arabic[9], hindi[9]);
        }

        internal static bool CheckConnection() {
            try {
                Ping ping = new Ping();
                string host = "selfservice.cf";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions options = new PingOptions();
                PingReply reply = ping.Send(host, timeout, buffer, options);
                return (reply.Status == IPStatus.Success);
            } catch (Exception) {
                return false;
            }
        }

        internal static string ReadPlanResource(string resource) {
            ResourceManager rm = new ResourceManager("SelfService.Screens.Plans.PlansResource", Assembly.GetExecutingAssembly());
            string str = rm.GetString(resource);
            return str;
        }

        internal static Image LoadImage(string image) {
            //Bitmap bitmap;
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //using (Stream stream = assembly.GetManifestResourceStream("SelfService.Images." + image)) {
            //    bitmap = new Bitmap(stream);
            //    stream.Close();
            //}
            //return bitmap;

            var file = Application.StartupPath + "\\Images\\" + image;
            if (File.Exists(file)) {
                return Bitmap.FromFile(file);
            }

            return null;
        }

        internal static Image LoadImageFromPath(string imageName) {
            var path = Application.StartupPath + "\\Images\\" + imageName;
            if (File.Exists(path)) {
                Image image = Image.FromFile(path);
                return image;
            }

            return null;
        }

        internal static void ShowToss(string text) {
            new Toss(text).Show();
        }

        internal static void PrintDataGrid(DataGridView grd, bool landscape = true) {
            Bitmap bitmap;
            int height = grd.Height;
            ScrollBars scroll = grd.ScrollBars;
            int selected = grd.SelectedRows[0].Index;

            grd.ScrollBars = ScrollBars.None;
            grd.Height = grd.RowCount * grd.RowTemplate.Height * 2;
            grd.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;

            bitmap = new Bitmap(grd.Width, grd.Height);
            grd.DrawToBitmap(bitmap, new Rectangle(0, 0, grd.Width, grd.Height));

            PrintDocument document = new PrintDocument {
                DefaultPageSettings = new PageSettings {
                    Landscape = landscape,
                    PaperSize = new PaperSize("A4", 825, 1175),
                    Margins = new Margins(50, 50, 50, 50)
                },
            };
            document.PrintPage += (s, e) => {
                if (landscape) {
                    e.Graphics.DrawImage(bitmap, 10, 10, 1155, grd.Height);
                } else {
                    e.Graphics.DrawImage(bitmap, 10, 10, 775, grd.Height);
                }
            };

            PrintPreviewDialog dialog = new PrintPreviewDialog {
                Document = document,
            };
            dialog.Show();

            grd.Height = height;
            grd.ScrollBars = scroll;
            grd.Rows[selected].Selected = true;
        }

        private static void Document_PrintPage(object sender, PrintPageEventArgs e) {
            throw new NotImplementedException();
        }

        internal static void PrintFooter(Graphics g) {
            Rectangle rect = new Rectangle(0, 1100, 800, 40);
            var email = DB.Execute.GetConfig("official-email");
            var phone = DB.Execute.GetConfig("official-phone");
            string footer = String.Format(Resources.PrintFooter, Tools.ToHindi(phone), email);
            StringFormat near = new StringFormat {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.DirectionRightToLeft,
            };
            using (Font font = new Font(Fonts.Arial, 10, FontStyle.Bold)) {
                g.DrawString(footer, font, Brushes.Black, rect, near);
            }
        }

        internal static void PrintStamp(Graphics g) {
            Tools.PrintStamp(g, new Rectangle(300, 950, 160, 160));
        }

        internal static void PrintStamp(Graphics g, Rectangle rect) {
            Image image = Tools.LoadImage("stamp.png");
            g.DrawImage(image, rect);
        }

        public static int PaperWidth { get => 827; }
        public static int PaperHeight { get => 1170; }
    }
}
