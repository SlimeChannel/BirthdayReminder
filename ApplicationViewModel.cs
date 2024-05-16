using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows;
using Newtonsoft.Json;
using System.Windows.Controls;
#pragma warning disable CS8601 
#pragma warning disable CS8602
#pragma warning disable CS8603

namespace BirthdayReminder
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BirthdayEntry> BirthdayEntries = new ObservableCollection<BirthdayEntry>();
        private BirthdayEntry selectedBirthdayEntry;
        //List<Order> SortedList = objListOrder.OrderBy(o => o.OrderDate).ToList(); фильтры сделать

        private RelayCommand? addCommand;
        public RelayCommand AddCommand => addCommand ??= new RelayCommand(obj =>
                                                         {
                                                             BirthdayEntry? birthdayEntry = new BirthdayEntry();
                                                             BirthdayEntries.Insert(0, birthdayEntry);
                                                             SelectedBirthdayEntry = birthdayEntry;
                                                         });
        public BirthdayEntry SelectedBirthdayEntry
        {
            get => selectedBirthdayEntry;
            set
            {
                selectedBirthdayEntry = value;
                OnPropertyChanged("SelectedBirthdayEntry");
            }
        }
        public ApplicationViewModel()
        {
            RefreshBirthdayList();
            CheckTime();
            selectedBirthdayEntry = BirthdayEntries[0];
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        public void RefreshBirthdayList()
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory + "/resources/birthdaysList.json";
            StreamReader? reader = new(directory);
            string json = File.Exists(directory) ? reader.ReadToEnd() : "[]";
            BirthdayEntries = JsonConvert.DeserializeObject<ObservableCollection<BirthdayEntry>>(json);
        }
        public void CheckTime()
        {
            if (BirthdayEntries != null) foreach (BirthdayEntry? birthdayEntry in BirthdayEntries)
                birthdayEntry.DaysLeft = new DateTime(DateTime.Now.Year, DateTime.Parse(birthdayEntry.Date).Month, DateTime.Parse(birthdayEntry.Date).Day).Subtract(DateTime.Now).Days % 365 + Convert.ToInt32(DateTime.IsLeapYear(DateTime.Now.Year));
        }
    }
}
