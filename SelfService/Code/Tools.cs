namespace SelfService.Code
{
    static class Tools
    {
        public static string ToHindi(this string input) {
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

        public static int PaperWidth { get => 827; }
        public static int PaperHeight { get => 1170; }
    }
}
