using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BirthdayReminder

{
    public class RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
    {
        private Action<object> execute = execute;
#pragma warning disable CS8601
        private readonly Func<object, bool> canExecute = canExecute;
#pragma warning restore CS8601

        public static event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool? CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

#pragma warning disable CS8604
        public void Execute(object? parameter) => this.execute(parameter);
#pragma warning restore CS8604
    }
}