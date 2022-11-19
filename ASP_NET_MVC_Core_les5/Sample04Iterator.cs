using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASP_NET_MVC_Core_les5
{
    internal class Sample04Iterator
    {
        static void Main(string[] args)
        {
            LogFileSource logFileSource = new LogFileSource("D:/logs/sample.log");
            foreach (LogEntry log in logFileSource)
            {
                Console.WriteLine(log);
            }

            Console.ReadKey();
        }
    }

    public class LogFileSource : IEnumerable<LogEntry>
    {
        private readonly string _fileName;
        //private List<LogEntry> _logs; 

        public LogFileSource (string fileName)
        {
            _fileName = fileName;
        }

        public IEnumerator<LogEntry> GetEnumerator()
        {
            foreach( var line in  File.ReadAllLines(_fileName))
            {
                yield return LogEntry.Parse(line);
            }
            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
