using ASP_NET_MVC_Core_l5hwdll;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASP_NET_MVC_Core_l5hw
{
    public class ScannerEmulator
    {

        private static MonitorDeviceContext monitorDeviceContext = new MonitorDeviceContext(new ScannerMonitorDataList());
        public static void ScannerSubscriber(string text)
        {
            Random rnd = new Random();
            IMonitorData monitorData = new ScannerMonitorData (rnd.Next(1, 99), rnd.Next(1, 99));
            monitorDeviceContext._monitoringSystemDevice.Add(monitorData);
            monitorDeviceContext.RunMonitorProcessLastItem();
            Console.WriteLine(text);            
        }
    }
}
