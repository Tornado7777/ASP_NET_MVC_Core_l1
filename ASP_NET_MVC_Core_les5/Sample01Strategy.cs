using System;
using System.Collections.Generic;

namespace ASP_NET_MVC_Core_les5
{
	internal class Sample01Strategy
    {
		static void Main(string[] args)
		{
			//LogProcessor logProcessor1 = new LogProcessor(new LogFileReader();
			//logProcessor1.Write("Test message.");

   //         LogProcessor logProcessor2 = new LogProcessor(new WindowsEventLogReader();
   //         logProcessor2.Write("Test message.");

			//������������������� � ������� ����� ������������ ��������� ����� (������������ � Func....)
			LogProcessor logProcessor3 = new LogProcessor(() => new WindowsEventLogReader().Read());
			logProcessor3.ProcessLogs();

			Console.ReadKey();
        }
	}

	//���������� ������� ����
	public class LogEntry
	{
		public string line;

		public LogEntry()
		{

		}
		public LogEntry (string line)
		{
			this.line = line;
		}

		public static LogEntry Parse (string line)
		{
			return new LogEntry(line);
		}

		public override string ToString()
		{
			return line;
		}
	}

	//��������� ����������� ����� ��������� ������ ����
	public interface ILogReader
	{
		public List<LogEntry> Read();
	}

	public interface ILogWriter
	{
		public void  Write(string message);

		public void WriteError(Exception exption);
	}

	//���������� ���������� ����������
	public class LogFileReader : ILogReader, ILogWriter
	{
		public List<LogEntry> Read()
		{
			throw new NotImplementedException();
		}

		public void Write(string message)
		{
			throw new NotImplementedException();
		}

		public void WriteError(Exception exption)
		{
			throw new NotImplementedException();
		}
	}

    public class WindowsEventLogReader : ILogReader, ILogWriter
    {
        public List<LogEntry> Read()
        {
			var list = new List<LogEntry>();
			list.Add(new LogEntry());
            list.Add(new LogEntry()); 
			list.Add(new LogEntry()); 
			list.Add(new LogEntry());
            return list;
        }

        public void Write(string message)
        {
            throw new NotImplementedException();
        }

        public void WriteError(Exception exption)
        {
            throw new NotImplementedException();
        }
    }

	//������ ���������
	public class LogProcessor
	{
		private ILogWriter _logWriter; //����

        //�������(���������) �� ������� ������������ List<LogEntry>
        private readonly Func<List<LogEntry>> _logImporter;

		public LogProcessor(Func<List<LogEntry>> logImporter) 
		{
			_logImporter = logImporter;
		}

		public LogProcessor(ILogWriter logWriter) //���
		{
			_logWriter = logWriter;

        }

        public void Write(string message) //����
        {
            _logWriter.Write(message);
        }

        public void WriteError(Exception exption) //����
        {
            _logWriter.WriteError(exption);
        }

		public void SetProcessor(ILogWriter logWriter) //���
        {
            _logWriter = logWriter;

        }

        public void ProcessLogs()
		{
			foreach(var logEntry in _logImporter.Invoke())
			{
				SaveLogEntry(logEntry);
			}
		}

		private void SaveLogEntry(LogEntry log)
		{
			Console.WriteLine("Save log entry");
		}
    }

}