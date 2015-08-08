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
        private bool _IsOnline;
        private int _UnreadMessagesCount = 0;

        public bool IsOnline
        {
            get
            {
                return _IsOnline;
            }
            set
            {
                if (_IsOnline != value)
                {
                    _IsOnline = value;
                    RaiseIsOnlineChanged("IsOnline");
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
                    RaiseIsOnlineChanged("UnreadMesssagesCount");
                }
            }
        }

        private async void RaiseIsOnlineChanged(string propertyName)
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
