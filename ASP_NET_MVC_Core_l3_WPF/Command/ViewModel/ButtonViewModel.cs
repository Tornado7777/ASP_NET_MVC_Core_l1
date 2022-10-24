using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ASP_NET_MVC_Core_l3_WPF.Command.ViewModel
{
    internal class ButtonViewModel
    {
        public FirstButtonCommand ButtonCommand { get; set; }
        public ButtonViewModel()
        {
            ButtonCommand = new FirstButtonCommand(this);
        }

        public void OnExecute()
        {
            MainWindow Form = Application.Current.Windows[0] as MainWindow;

            Form.textBlock1.Text = "Hello";

        }
    }
}
