using Xunit;
using System.IO;
using BlazorApp.Services;

namespace ArithmeticEvaluator.Tests.Services
{
    public class LoggingServiceTests : IDisposable
    {
        private readonly ILoggingService _loggingService;
        private readonly string testLogFilePath = "test_log.txt";

        public LoggingServiceTests()
        {
            _loggingService = new LoggingService(testLogFilePath);
            if (File.Exists(testLogFilePath))
            {
                File.Delete(testLogFilePath); // Ensure a clean slate for each test run
            }
        }

        [Fact]
        public void LogOperation_WritesToFile()
        {
            // Arrange
            var expression = "3+5";
            var result = "Eight"; // Updated to capitalize

            // Act
            _loggingService.LogOperation(expression, result);

            // Assert
            var logContent = File.ReadAllText(testLogFilePath);
            Assert.Contains($"{expression} = {result}", logContent);
        }

        [Fact]
        public void GetLogs_ReturnsContent()
        {
            // Arrange
            var expression = "10+20";
            var result = "Thirty"; // Ensure correct capitalization

            // Act
            _loggingService.LogOperation(expression, result);
            var logs = _loggingService.GetLogs();

            // Assert
            Assert.NotEmpty(logs);

            // Create the expected log entry with proper formatting
            var expectedLogEntry = $"{expression} = {char.ToUpper(result[0]) + result.Substring(1)}"; // Capitalize first letter of result
            Assert.Contains(expectedLogEntry, logs);
        }




        // Clean up after tests
        public void Dispose()
        {
            if (File.Exists(testLogFilePath))
            {
                File.Delete(testLogFilePath);
            }
        }
    }

}
