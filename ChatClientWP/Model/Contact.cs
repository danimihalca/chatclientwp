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

namespace ChatClientWP
{
    public class Contact : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        private bool _IsOnline;
        public bool IsOnline
        {
            get
            {
                return _IsOnline;
            }
            set
            {
                _IsOnline = value;
                Debug.WriteLine(UserName+"->"+_IsOnline);
                RaiseIsOnlineChanged();
            }
        }

        private async void RaiseIsOnlineChanged()
        {
            if (PropertyChanged != null)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ID"));
                    //CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace));
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
