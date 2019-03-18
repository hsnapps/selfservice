using SelfService.Code;
using System.Drawing;
using System.Windows.Forms;

namespace SelfService.Components
{
    class CharacterButton: Button
    {
        const int BTNWIDTH = 77;
        const int BTNHEIGHT = 68;

        public CharacterButton(char ar, char en, int x, int y) {
            BackColor = Color.White;
            FlatStyle = FlatStyle.Flat;
            Font = new Font(Fonts.ALMohanadBold, 25f);
            Location = new Point(x, y);
            Size = new Size(BTNWIDTH, 68);
            Cursor = Cursors.Hand;
            TabStop = false;
            Text = ar == '*' ? "لا" : ar.ToString();
            Tag = ar == '*' ? "لا" : ar.ToString();
            UseVisualStyleBackColor = false;

            if (ar == '@') {
                Text = ('\u00D2').ToString();
                Font = new Font(Fonts.Wingdings2, 25f);
                Ignore = true;
            }

            if (ar == '$') {
                Text = ('\u0037').ToString();
                Font = new Font(Fonts.Wingdings, 25f);
                Ignore = true;
            }

            ArabicChar = ar == '*' ? "لا" : ar.ToString();
            EnglishChar = en.ToString();
        }

        public string ArabicChar { get; private set; }
        public string EnglishChar { get; private set; }
        public bool Ignore { get; private set; }
    }
}
