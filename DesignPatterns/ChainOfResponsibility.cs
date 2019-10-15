using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.DesignPatterns
{
    public class ChainOfResponsibility : Pattern
    {

        #region Model
        public class LogInfo
        {
            public int Level { get; set; }
            public string Message { get; set; }
            public LogInfo(int level, string message)
            {
                Level = level;
                Message = message;
            }
        }
        #endregion

        #region Handler
        public interface ILogHandler
        {
            void SetNext(ILogHandler handler);
            void WriteLog(LogInfo log);
        }
        #endregion

        #region Base Handler
        public abstract class BaseLog : ILogHandler
        {
            private int logLevel;
            private ILogHandler _handler { get; set; }
            public BaseLog(int level) => logLevel = level;
            public void SetNext(ILogHandler handler)
            {
                _handler = handler;
            }
            public void WriteLog(LogInfo log)
            {
                if (logLevel >= log.Level) WriteMessage(log.Message);
                else _handler.WriteLog(log);
            }

            protected abstract void WriteMessage(string msg);
        }
        #endregion

        #region Concrete Handler
        public class InfoLog : BaseLog
        {
            public InfoLog(int level) : base(level) { }

            protected override void WriteMessage(string msg)
            {
                Console.WriteLine($"INFO: Message: {msg}");
            }
        }

        public class DebugLog : BaseLog
        {
            public DebugLog(int level) : base(level) { }
            protected override void WriteMessage(string msg)
            {
                Console.WriteLine($"DEBUG: Message: {msg}");
            }
        }

        public class ErrorLog : BaseLog
        {
            public ErrorLog(int level) : base(level) { }
            protected override void WriteMessage(string msg)
            {
                Console.WriteLine($"ERROR: Message: {msg}");
            }
        }
        #endregion

        /// <summary>
        /// Problem: When we have process that depend on recent step and recent step
        /// Solved: We use chain handler for manage this problem due to current handle can trigger the next handler or exit the opertaion
        /// </summary>
        public override void Demo()
        {
            var infoLog = new InfoLog(1);
            var debugLog = new DebugLog(2);
            var errorLog = new ErrorLog(3);
            infoLog.SetNext(debugLog);
            debugLog.SetNext(errorLog);
            infoLog.WriteLog(new LogInfo(1, "This is Info log"));
            infoLog.WriteLog(new LogInfo(2, "This is debug log"));
            infoLog.WriteLog(new LogInfo(3, "This is error log"));
        }
    }
}
