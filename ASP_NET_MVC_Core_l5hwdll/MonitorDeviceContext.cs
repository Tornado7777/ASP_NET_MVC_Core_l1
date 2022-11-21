using System;
using System.Collections.Generic;

namespace ASP_NET_MVC_Core_l5hwdll
{
    public interface IMonitorData
    {
        int Cpu { get;  }
        int Memory { get; }

        string ToString();
    }
    public interface IMonitoringSystemDevice
    {
        void Add(IMonitorData monitorData);
        IMonitorData ById(int id);
        IEnumerator<IMonitorData> GetEnumerator();
    }
    public interface IMonitorPipelineItem
    {
        void SetNextItem(IMonitorPipelineItem pipelineItem);
        void ProcessData(IMonitorData data);
    }

    public class ScannerMonitorData : IMonitorData
    {
        public int Cpu { get; }
        public int Memory { get; }

        public ScannerMonitorData (int cpu, int memory)
        {
            Cpu = cpu;
            Memory = memory;
        }

        public override string ToString()
        {
            return $"Cpu: {Cpu}; Memoty: {Memory} \n";
        }
    }
    public class ScannerMonitorDataList : IMonitoringSystemDevice 
    {
        private List<IMonitorData> _scannerMonitorDataList;

        public ScannerMonitorDataList ()
        {
            _scannerMonitorDataList = new List<IMonitorData> ();
        }
        public IEnumerator<IMonitorData> GetEnumerator()
        {
            foreach (var line in _scannerMonitorDataList)
            {
                yield return line;
            }

        }

        public void Add(IMonitorData monitorData)
        {
            _scannerMonitorDataList.Add(monitorData);
        }
        IEnumerator<IMonitorData> IMonitoringSystemDevice.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IMonitorData ById(int id)
        {
            return _scannerMonitorDataList[id];
        }
    }


    public abstract class MonitorPipelineItem : IMonitorPipelineItem
    {
        private IMonitorPipelineItem _next;
        public void SetNextItem(IMonitorPipelineItem pipelineItem)
        {
            _next = pipelineItem;
        }
        public void ProcessData(IMonitorData data)
        {
            if (ReviewData(data) && _next != null)
            {
                _next.ProcessData(data);
            }
        }
        protected abstract bool ReviewData(IMonitorData data);
    }
    public sealed class CpuMonitorPipelineItem : MonitorPipelineItem
    {
        protected override bool ReviewData(IMonitorData data)
        {
            if (data is null)
            {
                return false;
            }

            if (data.Cpu == 0)
            {
                return false;
            }
            //do some work
            Console.WriteLine($"CpuMonitorPipelineItem {data.Cpu} succes");
            return true;
        }
    }
    public sealed class MemoryMonitorPipelineItem : MonitorPipelineItem
    {
        protected override bool ReviewData(IMonitorData data)
        {
            if (data is null)
            {
                return false;
            }
            if (data.Memory == 0)
            {
                return false;
            }
            //do some work
            Console.WriteLine($"MemoryMonitorPipelineItem {data.Memory} succes");
            return true;
        }
    }
    public sealed class MonitorDeviceContext
    {
        public readonly IMonitoringSystemDevice _monitoringSystemDevice;
        private int _currentItem;
        public MonitorDeviceContext(IMonitoringSystemDevice
        monitoringSystemDevice)
        {
            _monitoringSystemDevice = monitoringSystemDevice;
            _currentItem = 0;
        }
        public void RunMonitorProcess()
        {
            IMonitorPipelineItem pipelineItem = CreatePipeline();
            foreach (IMonitorData data in _monitoringSystemDevice)
            {
                pipelineItem.ProcessData(data);
            }
        }

        public void RunMonitorProcessLastItem()
        {
            IMonitorPipelineItem pipelineItem = CreatePipeline();

            pipelineItem.ProcessData(_monitoringSystemDevice.ById(_currentItem));
        }
        private IMonitorPipelineItem CreatePipeline()
        {
            IMonitorPipelineItem cpuMonitorPipelineItem = new
            CpuMonitorPipelineItem();
            IMonitorPipelineItem memoryMonitorPipelineItem = new
            MemoryMonitorPipelineItem();

            cpuMonitorPipelineItem.SetNextItem(memoryMonitorPipelineItem);
            return cpuMonitorPipelineItem;
        }
    }
}
