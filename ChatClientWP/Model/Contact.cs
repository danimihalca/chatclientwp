using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace ChatClientWP.Model
{

    public class Contact :BaseUser, INotifyPropertyChanged
    {

        private USER_STATE _State;
        private int _UnreadMessagesCount = 0;

        public USER_STATE State
        {
            get
            {
                return _State;
            }
            set
            {
                if (_State != value)
                {
                    _State = value;
                    RaisePropertyChanged("State");
                }
            }
        }
        public int UnreadMesssagesCount
        {
            get
            {
                return _UnreadMessagesCount;
            }
            set
            {
                if (_UnreadMessagesCount != value)
                {
                    _UnreadMessagesCount = value;
                    RaisePropertyChanged("UnreadMesssagesCount");
                }
            }
        }

        private async void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
