using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace example1
{
    public enum DataType
    {
        Inbound,
        Outbound
    }

    public struct Data
    {
        public string Name;
        public DataType Type;
        public Data(string name, DataType type = DataType.Inbound)
        {
            Name = name;
            Type = type;
        }
    }

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

        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }

        private static int CompareGroups(Data x, Data y)
        {
            if (x.Name == null)
            {
                if (y.Name == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y.Name == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    int retval = x.Name.Length.CompareTo(y.Name.Length);

                    if (retval != 0)
                    {
                        // If the strings are not of equal length,
                        // the longer string is greater.
                        //
                        return retval;
                    }
                    else
                    {
                        // If the strings are of equal length,
                        // sort them with ordinary string comparison.
                        //
                        return x.Name.CompareTo(y.Name);
                    }
                }
            }
        }

        private void CreateMenus()
        {
            List<Data> groups = new List<Data>
            {
                new Data("AInbound1"),
                new Data("aInbound2"),
                new Data("PG_AOutbound1", DataType.Outbound),
                new Data("NInbound2"),
                new Data("AInbound3"),
                new Data("SInbound1"),
                new Data("vOutbound1", DataType.Outbound),
                new Data("vInbound1"),
                new Data("AOutbound1", DataType.Outbound),
            };
            MenuItems = new ObservableCollection<MenuItemViewModel>();
            foreach (var g in groups.GroupBy(d => d.Type))
            {
                var kind = new MenuItemViewModel
                {
                    Header = g.Key.ToString()
                };
                MenuItems.Add(kind);
                var elts = groups.Where(t => t.Type.Equals(g.Key));
                if (elts.Count() == 0)
                {
                    continue;
                }
                var eltsGroup = groups.GroupBy(n =>n.Name.ToUpper()[0]);
                kind.MenuItems = new ObservableCollection<MenuItemViewModel>();
                foreach (var eltGroup in eltsGroup)
                {
                    var cat = new MenuItemViewModel
                    {
                        Header = $"{eltGroup.Key}".ToUpper()
                    };
                    elts = groups.Where(t => t.Type.Equals(g.Key) && t.Name.ToUpper().StartsWith(cat.Header) && !t.Name.ToUpper().StartsWith("PG_"));
                    switch (elts.Count())
                    {
                        case 0:
                            continue;
                        case 1:
                            kind.MenuItems.Add(new MenuItemViewModel { Header = elts.First().Name });
                            break;
                        default:
                            kind.MenuItems.Add(cat);
                            cat.MenuItems = new ObservableCollection<MenuItemViewModel>();
                            foreach (var item in elts)
                            {
                                cat.MenuItems.Add(new MenuItemViewModel { Header = item.Name });
                            }
                            break;
                    }
                }
            }            
        }

        public MainWindow()
        {
            InitializeComponent();
            CreateMenus();

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

    public class MenuItemViewModel
    {
        private readonly ICommand _command;

        public MenuItemViewModel()
        {
            _command = new CommandViewModel(Execute);
        }

        public string Header { get; set; }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }

        public ICommand Command
        {
            get
            {
                return _command;
            }
        }

        private void Execute()
        {
            // (NOTE: In a view model, you normally should not use MessageBox.Show()).
            MessageBox.Show("Clicked at " + Header);
        }
    }

    public class CommandViewModel : ICommand
    {
        private readonly Action _action;

        public CommandViewModel(Action action)
        {
            _action = action;
        }

        public void Execute(object o)
        {
            _action();
        }

        public bool CanExecute(object o)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
    }
}
