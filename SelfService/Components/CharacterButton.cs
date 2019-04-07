using SelfService.Code;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Components
{
    class CharacterButton: Button
    {
        const int BTNWIDTH = 65;
        const int BTNHEIGHT = 60;

        public CharacterButton(char ar, char en, int x, int y) {
            BackColor = Color.White;
            FlatStyle = FlatStyle.Flat;
            Font = new Font(Fonts.ALMohanadBold, 25f);
            Location = new Point(x, y);
            Size = new Size(BTNWIDTH, BTNHEIGHT);
            Cursor = Cursors.Hand;
            TabStop = false;
            Text = ar == '*' ? "لا" : ar.ToString();
            Tag = ar == '*' ? "لا" : ar.ToString();
            UseVisualStyleBackColor = false;

            if (ar == '@') {
                Text = "";
                Tag = ('\u00D2').ToString();
                //Font = new Font(Fonts.Wingdings2, 25f);
                this.Image = Tools.LoadImage("Back.png");
                this.ImageAlign = ContentAlignment.MiddleCenter;
                Ignore = true;
            }

            if (ar == '$') {
                Text = "";
                Tag = ('\u0037').ToString();
                //Font = new Font(Fonts.Wingdings2, 25f);
                this.Image = Tools.LoadImage("Globe.png");
                this.ImageAlign = ContentAlignment.MiddleCenter;
                Ignore = true;
            }

            ArabicChar = ar == '*' ? "لا" : ar.ToString();
            EnglishChar = en.ToString();
        }

        // Spacebar
        public CharacterButton(int x, int y) {

        }

        public string ArabicChar { get; private set; }
        public string EnglishChar { get; private set; }
        public bool Ignore { get; private set; }

        public static int DefaultWidth { get => BTNWIDTH; }
        public static int DefaultHeight { get => BTNHEIGHT; }
        public static int XStep { get => BTNWIDTH + 10; }
        public static int YStep { get => BTNHEIGHT + 10; }
    }
}
