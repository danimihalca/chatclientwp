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

        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime _Date = DateTime.Now;
    }
}
