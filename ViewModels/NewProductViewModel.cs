using MVVMTest.Date;
using MVVMTest.Structur.Commands;
using MVVMTest.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MVVMTest.ViewModels
{
    class NewProductViewModel : BaseViewModel
    {
        public Product NewProduct { get; set; }
        public ICommand CancelCommand { get; }

        private void OnCancelCommandExecuted(object p)
        {
            
        }

        private bool CanCancelCommandExecuted(object p) => true;

        NewProductViewModel()
        {
            
        }
    }
}
