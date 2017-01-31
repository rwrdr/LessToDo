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
    public sealed partial class EditTask : Page
    {

        public EditTask()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            txtboxTitle.Text = ToDoTask.ToDoList[ToDoTask.CurrentTask].Title;
            txtboxNote.Text = ToDoTask.ToDoList[ToDoTask.CurrentTask].Note;
            datepicker.Date = ToDoTask.ToDoList[ToDoTask.CurrentTask].Date;
            timepicker.Time = ToDoTask.ToDoList[ToDoTask.CurrentTask].Date.TimeOfDay;
            checkbox.IsChecked = ToDoTask.ToDoList[ToDoTask.CurrentTask].Priority;
        }

        private void CancelAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void AcceptAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ToDoTask.EditTask(
                new ToDoTask(
                    ToDoTask.ToDoList[ToDoTask.CurrentTask].UID, 
                    txtboxTitle.Text, 
                    txtboxNote.Text, 
                    new DateTime(datepicker.Date.Year, datepicker.Date.Month, datepicker.Date.Day, timepicker.Time.Hours, timepicker.Time.Minutes, 0),
                    ToDoTask.ToDoList[ToDoTask.CurrentTask].Completed, 
                    (bool)checkbox.IsChecked));
            Frame.GoBack();
        }
    }
}
