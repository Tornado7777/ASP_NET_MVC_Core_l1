using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ASP_NET_MVC_Core_l3_WPF.Command
{
    internal class ButtonFabric
    {
        public static T Create<T>(object parameter) where T : ICommand, new()
        {
            T t = new T();
            t.Execute(parameter);
            return t;
        }
    }
}
