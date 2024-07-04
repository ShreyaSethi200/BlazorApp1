using System;
using System.Collections.Generic;
using System.IO;



namespace BlazorApp.Services
{
    public interface ILoggingService
    {
        void LogOperation(string expression, string result);
        List<string> GetLogs();
    }

    public class LoggingService : ILoggingService
    {
        private readonly string _logFilePath;

        public LoggingService(string logFilePath = "log.txt")
        {
            _logFilePath = logFilePath;
        }

        public void LogOperation(string expression, string result)
        {
            var logEntry = $"{DateTime.Now}: {expression} = {result}\n";
            File.AppendAllText(_logFilePath, logEntry);
        }

        public List<string> GetLogs()
        {
            if (File.Exists(_logFilePath))
            {
                return new List<string>(File.ReadAllLines(_logFilePath));
            }
            else
            {
                return new List<string> { "Log file is empty." };
            }
        }
    }


}
