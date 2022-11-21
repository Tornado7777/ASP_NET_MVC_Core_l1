using System;
using System.Collections.Generic;

namespace ASP_NET_MVC_Core_l5hwdll
{
    public interface IMonitorData
    {
        int Cpu { get;  }
        int Memory { get; }
    }
    public interface IMonitoringSystemDevice : IEnumerable<IMonitorData>
    {
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
            return true;
        }
    }
    public sealed class VoltageMonitorPipeline : MonitorPipelineItem
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

            return true;
        }
    }
    public sealed class MonitorDeviceContext
    {
        private readonly IMonitoringSystemDevice _monitoringSystemDevice;
        public MonitorDeviceContext(IMonitoringSystemDevice
        monitoringSystemDevice)
        {
            _monitoringSystemDevice = monitoringSystemDevice;
        }
        public void RunMonitorProcess()
        {
            IMonitorPipelineItem pipelineItem = CreatePipeline();
            foreach (IMonitorData data in _monitoringSystemDevice)
            {
                pipelineItem.ProcessData(data);
            }
        }
        private IMonitorPipelineItem CreatePipeline()
        {
            IMonitorPipelineItem cpuMonitorPipelineItem = new
            CpuMonitorPipelineItem();
            IMonitorPipelineItem voltageMonitorPipelineItem = new
            VoltageMonitorPipeline();

            cpuMonitorPipelineItem.SetNextItem(voltageMonitorPipelineItem);
            return cpuMonitorPipelineItem;
        }
    }
}
