using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace LessToDo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateTask : Page
    {
        public CreateTask()
        {
            this.InitializeComponent();
            Windows.Globalization.Calendar callendar = new Windows.Globalization.Calendar();
            datepicker.MinYear = callendar.GetDateTime();
            datepicker.Date = callendar.GetDateTime();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void AcceptAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            string title = txtboxTitle.Text;
            string note = txtboxNote.Text;
            DateTime date = new DateTime(datepicker.Date.Year, datepicker.Date.Month, datepicker.Date.Day, timepicker.Time.Hours, timepicker.Time.Minutes, 0);
            bool priority = (bool)checkbox.IsChecked;
            ToDoTask tsk = new ToDoTask(Guid.NewGuid(), title, note, date, false, priority);
            ToDoTask.AddTask(tsk);
            Frame.Navigate(typeof(MainPage));
        }

        private void CancelAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
