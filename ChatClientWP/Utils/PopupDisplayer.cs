using Coding4Fun.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.Utils
{
    public class PopupDisplayer
    {
        public static void DisplayPopup(string text)
        {
            Debug.WriteLine("DisplayPopup:" + text);
            ToastPrompt toastInstance = new ToastPrompt();
            toastInstance.Title = text;
            toastInstance.MillisecondsUntilHidden = 5000;
            toastInstance.Show();
        }
    }
}
