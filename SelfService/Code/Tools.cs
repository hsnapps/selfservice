using System;
using System.Drawing;
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

        internal static Bitmap LoadImage(string image) {
            Bitmap bitmap;
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("SelfService.Images." + image)) {
                bitmap = new Bitmap(stream);
                stream.Close();
            }
            return bitmap;
        }

        internal static Image LoadImageFromPath(string imageName) {
            var path = Application.StartupPath + "\\Images\\" + imageName;
            if (File.Exists(path)) {
                Image image = Image.FromFile(path);
                return image;
            }

            return null;
        }

        public static int PaperWidth { get => 827; }
        public static int PaperHeight { get => 1170; }
    }
}
