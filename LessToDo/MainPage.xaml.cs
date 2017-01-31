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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace LessToDo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool hide = false;
        
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            ToDoTask.ToDoList.CollectionChanged += ToDoTask.ToDoList_CollectionChanged;

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame.BackStack.Clear();
            SetDataContext();
        }

        private void SetDataContext()
        {
            ListView1.DataContext = null;
            ListView2.DataContext = null;
            if (hide)
            {
                ListView1.DataContext = ToDoTask.ToDoList.Where(w => w.Completed.Equals(false));
                ListView2.DataContext = ToDoTask.ToDoList.Where(w => w.Priority.Equals(true) && w.Completed.Equals(false));
            }
            else
            {
                ListView1.DataContext = ToDoTask.ToDoList;
                ListView2.DataContext = ToDoTask.ToDoList.Where(w => w.Priority.Equals(true));
            }
            
        }

        private void GoToSingleTaskView(object sender, ItemClickEventArgs e)
        {
            ToDoTask.CurrentTask = ToDoTask.ToDoList.IndexOf((ToDoTask)e.ClickedItem);
            Frame.Navigate(typeof(SingleTaskView));
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateTask));
        }

        private void HideAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            hide = !hide;
            if (hide)
            {
                //HideAppBarButton.Icon = new SymbolIcon(Symbol.AllApps);
                HideAppBarButton.Label = "Uncompleted";
            }
            else
            {
                //HideAppBarButton.Icon = new SymbolIcon(Symbol.ViewAll);
                HideAppBarButton.Label = "All";
            }
            SetDataContext();
        }
    }
}
