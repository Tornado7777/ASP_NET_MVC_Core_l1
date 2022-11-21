using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASP_NET_MVC_Core_l5hwdll
{
 
    public sealed class ScannerContext
    {
        private readonly IMonitorData _device;
        private IScanOutputStrategy _currentStrategy;
        public ScannerContext(IMonitorData device)
        {
            _device = device;
        }
        public void SetupOutputScanStrategy(IScanOutputStrategy strategy)
        {
            _currentStrategy = strategy;
        }

        public IMonitorData Get()
        {
            return _device;
        }
        public void Execute(string outputFileName = "")

        {
            if (_device is null)
            {
                throw new ArgumentNullException("Device can not be null");
            }
            if (_currentStrategy is null)
            {
                throw new ArgumentNullException("Current scan strategy can not be null");
            }
            if (string.IsNullOrWhiteSpace(outputFileName))
            {
                outputFileName = Guid.NewGuid().ToString();
            }
            _currentStrategy.ScanAndSave(_device, outputFileName);
        }
    }
    public interface IScanOutputStrategy
    {
        void ScanAndSave(IMonitorData scannerDevice, string
        outputFileName);
    }
    public sealed class txtScanOutputStrategy : IScanOutputStrategy
    {
        public void ScanAndSave(IMonitorData scannerDevice, string
        outputFileName)
        {
            //do txt output
           File.AppendAllText(outputFileName, scannerDevice.ToString());
        }
    }
    public sealed class ImageScanOutputStrategy : IScanOutputStrategy
    {
        public void ScanAndSave(IMonitorData scannerDevice, string
        outputFileName)
        {
            //do image outptut
        }
    }
}
