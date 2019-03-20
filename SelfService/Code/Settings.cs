using System;
using System.Drawing;

namespace SelfService.Code
{
    static class Settings
    {
        static Settings() {
            var backgroundFile = DB.Execute.GetConfig("background");
            var buttonUpFile = DB.Execute.GetConfig("button-up");
            var buttonDownFile = DB.Execute.GetConfig("button-down");
            var foreColor = DB.Execute.GetConfig("button-fore-color");
            var backButtonUpFile = DB.Execute.GetConfig("back-up");
            var backButtonDownFile = DB.Execute.GetConfig("back-down");
            var backForeColor = DB.Execute.GetConfig("back-fore-color");

            KnownColor color1 = (KnownColor)Enum.Parse(typeof(KnownColor), foreColor);
            ButtonForeColor = Color.FromKnownColor(color1);

            KnownColor color2 = (KnownColor)Enum.Parse(typeof(KnownColor), foreColor);
            BackButtonForeColor = Color.FromKnownColor(color2);

            ButtonUpImage = Tools.LoadImage(buttonUpFile);
            ButtonDownImage = Tools.LoadImage(buttonDownFile);
            Background = Tools.LoadImage(backgroundFile);
            BackButtonUpImage = Tools.LoadImage(backButtonUpFile);
            BackButtonDnImage = Tools.LoadImage(backButtonDownFile);
        }

        public static Image Background { get; private set; }
        public static Image ButtonUpImage { get; private set; }
        public static Image ButtonDownImage { get; private set; }
        public static Color ButtonForeColor { get; private set; }
        public static Color BackButtonForeColor { get; private set; }
        public static Image BackButtonUpImage { get; private set; }
        public static Image BackButtonDnImage { get; private set; }
    }
}
