using System;
using System.Collections.Generic;
using System.Text;

namespace MouseMoverAppTray.Utils
{
    public static class Helpers
    {
        public static bool isDigit(this KeyPressEventArgs e)
        {
            return char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar);
        }
        public static bool isGreaterThan(this KeyPressEventArgs e, int value)
        {
            return Convert.ToInt32(e.KeyChar.ToString()) > value;
        }

    }
}
