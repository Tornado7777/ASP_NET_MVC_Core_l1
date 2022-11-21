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
            ScannerContext scannerContext = new ScannerContext(
                new ScannerMonitorData(rnd.Next(1, 99), rnd.Next(1, 99))
                );
            scannerContext.SetupOutputScanStrategy(new txtScanOutputStrategy());

            monitorDeviceContext._monitoringSystemDevice.Add(scannerContext.Get());
            monitorDeviceContext.RunMonitorProcessLastItem();

            scannerContext.Execute("D:/logs/sample2.txt");


            Console.WriteLine(text);            
        }
    }
}
