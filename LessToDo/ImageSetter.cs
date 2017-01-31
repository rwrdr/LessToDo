using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace LessToDo
{
    public class ImageSetter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var taskUID = (Guid)value;
            var path = String.Empty;
            foreach (ToDoTask x in ToDoTask.ToDoList)
            {
                if (x.UID == taskUID)
                    if (x.Completed)
                        return "Assets/check.png";
                    else if (x.Priority)
                        return "Assets/favs.png";
                    else
                        return "Assets/blank.png";
            }
            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
