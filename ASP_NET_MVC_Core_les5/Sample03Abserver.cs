using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace ASP_NET_MVC_Core_les5
{
    internal class Sample03Abserver
    {
        //Example pattren abserver -наблюдатель

        static void Subscriber(string text)
        {
            Console.WriteLine(text);
        }
        static void Main(string[] args)
        {
            using MyLogFileReader myLogFileReader = new MyLogFileReader("D:/logs/sample.log", Subscriber);

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
            foreach(var logEntry in ReadNewLogEntries())
            {
                _logEntrySubscribe(logEntry);
            }
        }

        private IEnumerable<string> ReadNewLogEntries()
        {
            while(!_streamReader.EndOfStream)
            {
               yield return  _streamReader.ReadLine();
            }
        }
    }
}
