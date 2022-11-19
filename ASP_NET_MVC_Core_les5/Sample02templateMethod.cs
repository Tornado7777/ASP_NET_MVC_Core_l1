using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASP_NET_MVC_Core_les5
{
    //Шаблоннный метод
    internal class Sample02templateMethod
    {
        static void Main(string[] args)
        {
            var reader = new BaseReader();
            var logs = reader.ReadLogEntries();

            Console.ReadKey();
        }
    }

    public abstract class LogReader
    {
        private int _currentPosition;
        public IEnumerable<LogEntry> ReadLogEntries()
        {
            return ReadEntries(ref _currentPosition).Select(ParseLogentry);
        }
        protected abstract IEnumerable<string> ReadEntries(ref int position);
        protected abstract LogEntry ParseLogentry(string strinEntry);
    }

    public class BaseReader : LogReader
    {
        protected override LogEntry ParseLogentry(string strinEntry)
        {
            //TODO
            return new LogEntry();
        }

        protected override IEnumerable<string> ReadEntries(ref int position)
        {
            //TODO....
            List<string> list = new List<string>();
            list.Add($"Line {position++}");
            list.Add($"Line {position++}");
            list.Add($"Line {position++}");
            list.Add($"Line {position++}");
            list.Add($"Line {position++}");
            list.Add($"Line {position++}");
            list.Add($"Line {position++}");
            return list;
        }
    }
}
