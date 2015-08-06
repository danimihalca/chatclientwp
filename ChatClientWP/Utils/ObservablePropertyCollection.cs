using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWP.Utils
{
    class ObservablePropertyCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged 
    {
        public ObservablePropertyCollection()
        {
            this.CollectionChanged += items_CollectionChanged;
        }


        public ObservablePropertyCollection(IEnumerable<T> collection)
            : base(collection)
        {
            this.CollectionChanged += items_CollectionChanged;
            foreach (INotifyPropertyChanged item in collection)
            item.PropertyChanged += item_PropertyChanged;

        }

        private void items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e != null)
            {
                if(e.OldItems!=null)
                    foreach (INotifyPropertyChanged item in e.OldItems)
                        item.PropertyChanged -= item_PropertyChanged;

                if(e.NewItems!=null)
                    foreach (INotifyPropertyChanged item in e.NewItems)
                        item.PropertyChanged += item_PropertyChanged;
            }
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var reset = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace,sender,sender,base.IndexOf((T)sender));
            this.OnCollectionChanged(reset);
        }
    }
}
