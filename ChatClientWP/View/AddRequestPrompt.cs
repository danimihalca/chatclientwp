using Coding4Fun.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ChatClientWP.View
{
    public class AddRequestPrompt
    {
        private MessagePrompt m_prompt;
        
        public bool IsOpen
        {
            get { return m_prompt.IsOpen;}
        }

        public bool Accepted;

        public AddRequestPrompt(string userName)
        {
            m_prompt = new MessagePrompt
            {
                Title = "Advanced Message",
                Body = new TextBlock
                {
                    Text = userName + " wants to add you as a contact",
                    Foreground = new SolidColorBrush(Colors.Gray),
                    FontSize = 30.0,
                    TextWrapping = TextWrapping.Wrap
                },
                IsCancelVisible = true
            };
            m_prompt.Completed += OnCompleted;
        }
        
        public void Show()
        {
            m_prompt.Show();
        }

        private void OnCompleted(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.PopUpResult == PopUpResult.Ok)
            {
                Accepted = true;
            }
            else
            {
                Accepted = false;
            }
        }
    }
}
