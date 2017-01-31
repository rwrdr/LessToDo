using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace LessToDo
{
    // Single ToDo Item
    public class ToDoTask
    {
        public Guid UID { get; private set; }
        public string Title { get; private set; }
        public string Note { get; private set; }
        public DateTime Date { get; private set; }
        public bool Completed { get; private set; }
        public bool Priority { get; private set; }

        public static ObservableCollection<ToDoTask> ToDoList = new ObservableCollection<ToDoTask>();

        public static int CurrentTask = new int();

        public ToDoTask() { }

        public ToDoTask(Guid uid, string title, string note, DateTime date, bool completed, bool priority)
        {
            this.UID = uid;
            this.Title = title;
            this.Note = note;
            this.Date = date;
            this.Completed = completed;
            this.Priority = priority;
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal static async void ToDoList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
        }

        public static void SortByDate()
        {
            var tmp = ToDoList[CurrentTask];
            List<ToDoTask> sortedList = ToDoList.OrderBy(i => i.Date).ToList();
            ToDoList.Clear();
            foreach (var sortedItem in sortedList)
                ToDoList.Add(sortedItem);
            CurrentTask = ToDoList.IndexOf(tmp);
        }

        public static async void AddTask(ToDoTask tsk)
        {
            ToDoList.Add(tsk);
            SortByDate();
            await saveData();
        }

        public static void DeleteTask()
        {
            ToDoList.RemoveAt(CurrentTask);
            saveData();
        }

        public static void ChangePriority()
        {
            ToDoList[CurrentTask].Priority = !(ToDoList[CurrentTask].Priority);

            ToDoList[CurrentTask].NotifyPropertyChanged();

            saveData();
        }

        public static void ChangeComplete()
        {
            ToDoList[CurrentTask].Completed = !(ToDoList[CurrentTask].Completed);

            ToDoList[CurrentTask].NotifyPropertyChanged();

            saveData();
        }

        public static void EditTask(ToDoTask tsk)
        {
            ToDoList[CurrentTask] = tsk;
            SortByDate();
            saveData();
        }

        public static void GenerateSmapleData()
        {
            AddTask(new ToDoTask(Guid.NewGuid(), "tt1", "nn1", new DateTime(2017, 3, 2), false, true));
            AddTask(new ToDoTask(Guid.NewGuid(), "tt2", "nn2", new DateTime(2017, 4, 5), false, true));
            AddTask(new ToDoTask(Guid.NewGuid(), "tt3", "nn3", new DateTime(2015, 7, 21), true, true));
            AddTask(new ToDoTask(Guid.NewGuid(), "tt4", "nn4", new DateTime(2017, 2, 27), false, false));
            AddTask(new ToDoTask(Guid.NewGuid(), "tt5", "nn5", new DateTime(2017, 4, 14), true, false));
            AddTask(new ToDoTask(Guid.NewGuid(), "tt7", "nn7", new DateTime(2017, 12, 2), false, false));
            AddTask(new ToDoTask(Guid.NewGuid(), "5 paragraphs of Lorem Ipsum", "Dolores nemo exercitationem qui libero quis dolores consequatur fuga. Quo corrupti et quaerat omnis. Quis corporis eos accusamus repellendus earum nesciunt. Maiores hic dicta aut autem illum quia. Molestiae occaecati totam est quos.Nam dignissimos facere officiis vitae corrupti.Rerum sed beatae eligendi quaerat enim.Quia laborum laboriosam soluta perferendis sed.", new DateTime(2017, 6, 26), false, false));
        }

        
        public static async Task loadData()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile textFile = await localFolder.GetFileAsync("data.json");

                using (IRandomAccessStream textStream = await textFile.OpenReadAsync())
                {
                    using (DataReader textReader = new DataReader(textStream))
                    {
                        uint textLength = (uint)textStream.Size;
                        await textReader.LoadAsync(textLength);
                        string jsonContents = textReader.ReadString(textLength);
                        Debug.WriteLine(jsonContents);
                        JArray a = JArray.Parse(jsonContents);
                        foreach (JObject o in a.Children<JObject>())
                        {
                            Debug.WriteLine(o);
                            ToDoList.Add(new ToDoTask(new Guid(o["UID"].ToString()), o["Title"].ToString(), o["Note"].ToString(), Convert.ToDateTime(o["Date"].ToString()), (bool)o["Completed"], (bool)o["Priority"]));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }
            if (ToDoList.Count == 0)
                GenerateSmapleData();
        }

        public static async Task saveData()
        {
            var jsonContents = JsonConvert.SerializeObject(ToDoList);
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            try
            {
                StorageFile textFile = await localFolder.CreateFileAsync("data.json", CreationCollisionOption.ReplaceExisting);

                using (IRandomAccessStream textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    using (DataWriter textWriter = new DataWriter(textStream))
                    {
                        textWriter.WriteString(jsonContents);
                        await textWriter.StoreAsync();
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }
        }

        public override string ToString()
        {
            return this.Title + " at " + this.Date.ToString();
        }
    }
}
