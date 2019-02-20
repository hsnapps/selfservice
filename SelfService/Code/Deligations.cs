using System;
using System.Windows.Forms;

namespace SelfService.Code
{
    public delegate void InputGotFocusCallback(object s, EventArgs e);
    public delegate void KeyboardClickedCallback(object s, KeyPressEventArgs e);
    public delegate void EmailSentCallback(object s, EventArgs e);
}
