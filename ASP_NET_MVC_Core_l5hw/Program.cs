using ASP_NET_MVC_Core_l5hw;
using ASP_NET_MVC_Core_l5hwdll;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace ASP_NET_MVC_Core_l5hw
{
    /*
     * Cделайте эмулятор устройства сканера. Он сканирует (берет данные из какого либо файла),
     * производит фейковые данные о загрузке процессора и памяти. Код должен быть прост, и
     * дальнейшую работу стоит вести только с контрактами данного устройства. Разработать
     * небольшую библиотеку, которая принимает от этого эмулятора байты, сохраняет в различные
     * форматы и мониторит его состояние, записывая в какой-либо лог.
     */
    internal class Program
    {
        
        static void Main(string[] args)
        {
            ScannerEmulator scannerEmulator = new ScannerEmulator();

            using MyLogFileReader myLogFileReader = new MyLogFileReader(
                "D:/logs/sample.log",ScannerEmulator.ScannerSubscriber);

            Console.ReadKey();
        }

        

    }

    public class MyLogFileReader : IDisposable
    {
        private readonly Action<string> _logEntrySubscribe;
        private readonly string _logFileName;
        private readonly StreamReader _streamReader;
        private readonly FileStream _fileStream;
        private readonly Timer _timer;
        private readonly TimeSpan CheckFileInterval = TimeSpan.FromSeconds(5);

        public MyLogFileReader(string logFileName, Action<string> logEntrySubscribe)
        {
            if (!File.Exists(logFileName))
                throw new FileNotFoundException();

            _logEntrySubscribe = logEntrySubscribe;

            _logFileName = logFileName;
            _fileStream = new FileStream(_logFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _streamReader = new StreamReader(_fileStream);

            _timer = new Timer(f => CheckFile(), null, CheckFileInterval, CheckFileInterval);
        }

        public void Dispose()
        {
            _timer.Dispose();
            _streamReader.Dispose();
            _fileStream.Dispose();

        }

        private void CheckFile()
        {
            foreach (var logEntry in ReadNewLogEntries())
            {
                _logEntrySubscribe(logEntry);
            }
        }

        private IEnumerable<string> ReadNewLogEntries()
        {
            while (!_streamReader.EndOfStream)
            {
                yield return _streamReader.ReadLine();
            }
        }
    }
}

