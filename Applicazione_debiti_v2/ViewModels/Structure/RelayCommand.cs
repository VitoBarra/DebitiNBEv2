﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppDebitiV2.ViewModels
{
    public class RelayCommand : ICommand  // server per seprare la view dagli eventi click
    {
        private Action<object> executeMethod;
        private Predicate<object> CanExecuteChanfed;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> action, Predicate<object> predicate)
        {
            executeMethod = action;
            CanExecuteChanfed = predicate;
        }

        public RelayCommand(Action<object> action) : this(action, null) { }



        public bool CanExecute(object parameter) => (CanExecuteChanfed == null) ? true : CanExecuteChanfed.Invoke(parameter);

        public void Execute(object parameter) => executeMethod?.Invoke(parameter);

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);



    }
}
