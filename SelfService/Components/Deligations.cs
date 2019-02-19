using System;
using System.Windows.Forms;

namespace SelfService.Components
{
    public delegate void InputGotFocusCallback(object s, EventArgs e);
    public delegate void KeyboardClickedCallback(object s, KeyPressEventArgs e);
}
