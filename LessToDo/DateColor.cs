using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace LessToDo
{
    public class DateColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var date = (DateTime)value;
            var color = string.Empty;
            var dateNow = new Windows.Globalization.Calendar();
            if (date < dateNow.GetDateTime())
                color = "Red";
            else
                color = "White";
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
