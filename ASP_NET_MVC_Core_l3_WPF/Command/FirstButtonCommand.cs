using ASP_NET_MVC_Core_l3_WPF.Command.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ASP_NET_MVC_Core_l3_WPF.Command
{
    /*
    * Используя знания о паттернах, выберите приглянувшийся вам и создайте ICommand для WPF.
    */


    internal class FirstButtonCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        ButtonViewModel _buttonViewModel;

        public FirstButtonCommand(ButtonViewModel buttonViewModel)
        {
            _buttonViewModel = buttonViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _buttonViewModel.OnExecute();
        }
    }
}
