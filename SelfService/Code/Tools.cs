using SelfService.Components;
using SelfService.Properties;
using SelfService.Screens;
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
                        .Replace(arabic[9], hindi[9])
                        .Replace("AM", "ص")
                        .Replace("PM", "م");
        }

        internal static string LoadSound() {
            var file = Application.StartupPath + "\\Sounds\\soundtrack.wav";
            if (File.Exists(file)) {
                return file;
            }

            return null;
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

        internal static void PrintDataGrid(DataGridView grd, string title, int headerHeigt, int rowHeigt) {
            PageSettings pageSettings = GetPrinterPageInfo(null);
            Size paperSize = new Size(pageSettings.PaperSize.Height, pageSettings.PaperSize.Width);
            int leftMargin = GetMargin("LEFT");
            int rightMargin = GetMargin("RIGHT") * 2;

            PrintDocument document = new PrintDocument {
                DefaultPageSettings = new PageSettings {
                    Landscape = true,
                    // PaperSize = new PaperSize("A4", paperSize.Width, paperSize.Height),
                    Margins = new Margins(50, 50, 50, 50)
                },
            };
            document.PrintPage += (s, e) => {
                Graphics g = e.Graphics;
                Font font = new Font(Fonts.ALMohanad, 13);
                Font font12 = new Font(Fonts.ALMohanad, 12.5f);
                Font font18 = new Font(Fonts.ALMohanadBold, 18, FontStyle.Bold);
                int x = leftMargin;
                int y = 50;
                int width = paperSize.Width - rightMargin;
                StringFormat format = new StringFormat {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.DirectionRightToLeft
                };
                Rectangle rect = new Rectangle(leftMargin, y, width, 25);
                //e.Graphics.DrawRectangle(Pens.Black, rect);

                #region Draw Right Header
                g.DrawString(Resources.TVTC_Ar_Full, font, Brushes.Black, rect, format);
                rect.Y += 25;
                g.DrawString(Resources.College_Ar, font, Brushes.Black, rect, format);
                rect.Y += 25;
                g.DrawString(Resources.AppTitle, font, Brushes.Black, rect, format);
                #endregion

                #region Draw Title
                format.Alignment = StringAlignment.Center;
                rect.Y += 60;
                g.DrawString(title, font18, Brushes.Black, rect, format);
                #endregion

                #region Draw Name and Section
                format.Alignment = StringAlignment.Center;
                rect.Y += 25;
                string name = String.Format("{0} - {1}", BaseForm.Student.Name_AR, BaseForm.Student.Section);
                g.DrawString(name, font12, Brushes.Black, rect, format);
                #endregion

                #region Render Logo
                Image logo = Tools.LoadImage("Logo.png");
                g.DrawImage(logo, new PointF(leftMargin, 50));
                #endregion

                #region Draw Grid Headers
                rect.Y += 35;
                rect.Height = 70;
                for (int i = grd.Columns.Count - 1; i > -1; i--) {
                    format.Alignment = StringAlignment.Center;
                    rect.Width = width / grd.Columns.Count;
                    g.DrawRectangle(Pens.Black, rect);
                    g.FillRectangle(Brushes.LightGray, rect);
                    g.DrawString(grd.Columns[i].HeaderText, font12, Brushes.Black, rect, format);
                    rect.X = rect.Right;
                }
                #endregion

                #region Draw Grid Rows
                rect.Y = rect.Bottom;
                rect.X = leftMargin;
                rect.Height = 30;
                for (int i = 0; i < grd.Rows.Count; i++) {
                    for (int k = grd.Columns.Count - 1; k > -1; k--) {
                        rect.Width = width / grd.Columns.Count;
                        g.DrawRectangle(Pens.Black, rect);
                        string value = ToHindi(grd.Rows[i].Cells[k].Value.ToString());
                        g.DrawString(value, font12, Brushes.Black, rect, format);
                        rect.X = rect.Right;
                    }
                    rect.Y = rect.Bottom;
                    rect.X = leftMargin;
                } 
                #endregion

                font.Dispose();
                font12.Dispose();
                font18.Dispose();
            }; // -- END PrintPage

#if DEBUG
            PrintPreviewDialog dialog = new PrintPreviewDialog {
                Document = document,
                TopLevel = true,
            };
            dialog.Show();
#else
            document.Print();
#endif
        }

        private static int GetMargin(string v) {
            var path = Application.StartupPath + "\\DB\\print.txt";
            int margin = 0;

            if (File.Exists(path)) {
                var lines = File.ReadLines(path);

                foreach (var line in lines) {
                    if (line.StartsWith(v)) {
                        var parts = line.Split('=');
                        if (parts.Length < 2) break;
                        var s_margin = parts[1].Trim();
                        Int32.TryParse(s_margin, out margin);
                        break;
                    }
                }
            }

            return margin;
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

        private static PageSettings GetPrinterPageInfo(String printerName) {
            PrinterSettings settings;

            // If printer name is not set, look for default printer
            if (String.IsNullOrEmpty(printerName)) {
                foreach (var printer in PrinterSettings.InstalledPrinters) {
                    settings = new PrinterSettings();
                    settings.PrinterName = printer.ToString();
                    if (settings.IsDefaultPrinter)
                        return settings.DefaultPageSettings;
                }
                return null; // <- No default printer  
            }

            // printer by its name 
            settings = new PrinterSettings();

            settings.PrinterName = printerName;
            return settings.DefaultPageSettings;
        }

        public static int PaperWidth { get => 827; }
        public static int PaperHeight { get => 1170; }
    }
}
