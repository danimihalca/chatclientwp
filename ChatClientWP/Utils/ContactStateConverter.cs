using ChatClientWP.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ChatClientWP.Utils
{
    public class ContactStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            USER_STATE myEnumValue = (USER_STATE)value;
            Debug.WriteLine(myEnumValue.ToString());
            return myEnumValue.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return USER_STATE.OFFLINE;
        }
    }
}
