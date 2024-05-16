using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BirthdayReminder
{
    public class BirthdayEntry : INotifyPropertyChanged
    {
        private DateOnly date;
        private string? name;
        private string? description;
        private List<Tag>? tags;
        private string? imagePath;
        private int daysLeft;

        public string Date
        {
            get { return date.ToString("dd.MM.yyyy"); }
            set
            {
                date = DateOnly.Parse(value);
                OnPropertyChanged("Date");
            }
            
        }
        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string? Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public List<Tag>? Tags
        {
            get { return tags; }
            set
            {
                tags = value;
                OnPropertyChanged("Tags");
            }
        }
        public string? ImagePath
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "resources/" + imagePath; }
            set
            {
                imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }
        public int DaysLeft
        {
            get { return daysLeft; }
            set
            {
                daysLeft = value > 0 ? value : value + 365;
                OnPropertyChanged("DaysLeft");
            }
        }
        public string DaysLeftStringBig
        {
            get
            {
                var titles = new[] { " день остался", " дня осталось", " дней осталось" };
                var cases = new[] { 2, 0, 1, 1, 1, 2 };
                return daysLeft + titles[daysLeft % 100 > 4 && daysLeft % 100 < 20 ? 2 : cases[(daysLeft % 10 < 5) ? daysLeft % 10 : 5]];
            }
            set { }
        }
        public string DaysLeftString
        {
            get
            {
                var titles = new[] { " день", " дня", " дней" };
                var cases = new[] { 2, 0, 1, 1, 1, 2 };
                return daysLeft + titles[daysLeft % 100 > 4 && daysLeft % 100 < 20 ? 2 : cases[(daysLeft % 10 < 5) ? daysLeft % 10 : 5]];
            }
            set { }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
