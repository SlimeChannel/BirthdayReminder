using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
#pragma warning disable CS8602
#pragma warning disable CS8622

namespace BirthdayReminder
{
    public partial class MainWindow : Window
    {
        public ApplicationViewModel dataContext = new();
        public MainWindow()
        {
            this.DataContext = dataContext;
            this.Initialized += MainWindow_Initialized;
            this.Closed += MainWindow_Closed;
            InitializeComponent();
            list1.ItemsSource = dataContext.BirthdayEntries;
            list1.SelectionChanged += list1_SelectedIndexChanged;
        }
        private void list1_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Title = ((BirthdayEntry)list1.SelectedItem).Name as string;
        }

        private void MainWindow_Initialized(object? sender, EventArgs e)
        {
            System.Timers.Timer timer = new System.Timers.Timer(360000);
            timer.Elapsed += CheckTimeElapsed;
        }
        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            StreamWriter? writer = new(AppDomain.CurrentDomain.BaseDirectory + "/resources/birthdaysList.json");
            try
            { File.Delete("resources/birthdayList.json"); }
            catch (Exception) { }
            writer.WriteLine(JsonConvert.SerializeObject(dataContext.BirthdayEntries));
        }
        private void CheckTimeElapsed(Object source, System.Timers.ElapsedEventArgs e)
        {
            dataContext.CheckTime();
        }
    }
}