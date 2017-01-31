using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class SingleTaskView : Page
    {

        public SingleTaskView()
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
            SingleGrid.DataContext = ToDoTask.ToDoList[ToDoTask.CurrentTask];
            CompleteIcon();
            PriorityIcon();
        }

        private void DeleteAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ToDoTask.DeleteTask();
            Frame.GoBack();
        }

        private void EditAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditTask));
        }

        private void PriorityAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ToDoTask.ChangePriority();
            PriorityIcon();
        }

        private void CompleteAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ToDoTask.ChangeComplete();
            CompleteIcon();
        }

        private void PriorityIcon()
        {
            if (ToDoTask.ToDoList[ToDoTask.CurrentTask].Priority)
                PriorityAppBarButton.Icon = new SymbolIcon(Symbol.Favorite);
            else
                PriorityAppBarButton.Icon = new SymbolIcon(Symbol.OutlineStar);
        }

        private void CompleteIcon()
        {
            if (ToDoTask.ToDoList[ToDoTask.CurrentTask].Completed)
                CompleteAppBarButton.Icon = new SymbolIcon(Symbol.SelectAll);
            else
                CompleteAppBarButton.Icon = new SymbolIcon(Symbol.Placeholder);
        }
    }
}
