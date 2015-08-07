using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.Model
{
    public class Message: INotifyPropertyChanged
    {
        public BaseUser Sender { get; set; }
        public BaseUser Receiver { get; set; }
        public string MessageText { get; set; }

        public string SenderName
        {
            get
            {
                return Sender.FullName;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
