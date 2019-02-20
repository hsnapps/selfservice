using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelfService.Components
{
    class Keyboard : Form
    {
        readonly Control control;
        List<Button> keys;

        readonly string[] rows = new string[4];
        readonly string[] shiftRows = new string[4];

        public Keyboard(Control control) {
            this.control = control;

            rows[0] = @"ذ1234567890-=";
            rows[1] = @"ضصثقفغعهخحجد\";
            rows[2] = @"شسيبلاتنمكط";
            rows[3] = @"ئءؤرلاىةوزظ";

            shiftRows[0] = @"ّ!@#$%^&*)(_+";
            shiftRows[1] = @"ًٌَُلإإ‘÷×؛<>|";
            shiftRows[2] = @"ٍِ][لأأـ،/:";
            shiftRows[3] = @"";
        }
    }
}
