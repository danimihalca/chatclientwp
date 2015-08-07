using Coding4Fun.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.Utils
{
    public class PopupDisplayer
    {
        public static void DisplayPopup(string text)
        {
            ToastPrompt t = new ToastPrompt();
            t.Title = text;
            t.MillisecondsUntilHidden = 2000;
            t.Show();
        }
    }
}
