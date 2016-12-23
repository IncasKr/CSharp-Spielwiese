using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace example1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyCollectionChanged
    {
        private ObservableCollection<TaskToDo> listOfTasks = new ObservableCollection<TaskToDo>();
        public ObservableCollection<TaskToDo> ListOfTasks
        {
            get
            {
                return listOfTasks;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            List<TaskToDo> thingsToDo = new List<TaskToDo>
            {
            new TaskToDo { ID = 1, Task = new TTask { CodeName = "ABA", Description = "Aus Bett aufstehen" } },
            new TaskToDo { ID = 2, Task = new TTask { CodeName = "ZP", Description = "Zähne putzen"} },
            new TaskToDo { ID = 1, Task = new TTask { Priority = 2, CodeName = "PG", Description = "Pflanze gießen"} },
            new TaskToDo { ID = 3, Task = new TTask { CodeName = "UKN", Description = ""} },
            };
            foreach (var item in thingsToDo)
            {
                ListOfTasks.Add(item);
            }
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string nomPropriete)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));
        }

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add
            {
                NotifyPropertyChanged("Task");
            }

            remove
            {
                NotifyPropertyChanged("Task");
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            TaskToDo tempTask;
            try
            {
                tempTask = ListOfTasks.First<TaskToDo>(item => item.Task.Description == "");
            }
            catch (Exception)
            {

                tempTask = null;
            } 
            if (tempTask != null)
            {
                tempTask.Task = new TTask { CodeName = "DG", Priority = 3, Description = "Duschen" };
            }
        }

        private void btnPriority_Click(object sender, RoutedEventArgs e)
        {
            foreach (TaskToDo element in listOfTasks)
            {
                element.ID++;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ListOfTasks.Count(etask => etask.Task.Description == "Frühstücken") == 0)
            {
                ListOfTasks.Add(new TaskToDo { ID = 1, Task = new TTask { CodeName = "EF", Description = "Frühstücken" } });
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var tempTask = ListOfTasks.Single(item => item.Task.Description == "Duschen");
            if (tempTask != null)
            {
                tempTask.Task.Description = "";
            }
        }
    }

    public class TaskToDo : INotifyPropertyChanged
    {
        private int id;
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                if (id == value)
                    return;
                id = value;
                NotifyPropertyChanged("ID");
            }
        }

        //public string CodeName
        //{
        //    get
        //    {
        //        return codeName;
        //    }
        //    set
        //    {
        //        if (codeName == value)
        //            return;
        //        codeName = value;
        //        NotifyPropertyChanged("CodeName");
        //    }
        //}
        //private string codeName;

        private TTask task;
        public TTask Task
        {
            get
            {
                return task;
            }
            set
            {
                if (task == value)
                    return;
                task = value;
                NotifyPropertyChanged("Task");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string nomPropriete)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));
        }
    }

    public class TTask : INotifyPropertyChanged
    {
        public string CodeName
        {
            get
            {
                return codeName;
            }
            set
            {
                if (codeName == value)
                    return;
                codeName = value;
                NotifyPropertyChanged("CodeName");
            }
        }
        private string codeName;

        public int Priority
        {
            get
            {
                return priority;
            }
            set
            {
                if (priority == value)
                    return;
                priority = value;
                NotifyPropertyChanged("Priority");
            }
        }
        private int priority;

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description == value)
                    return;
                description = value;
                NotifyPropertyChanged("Description");
            }
        }
        private string description;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string nomPropriete)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));
        }

        public TTask()
        {
            Priority = 1;
        }
    }
}
