using log4net;
using OHM.Logger;
using System;

namespace ConsoleApplication1.Logger
{
    public class ConsoleLogger : DefaultLogger, ILogger
    {
       
        public ConsoleLogger(ILog log)
            : base(log)
        {
            
        }

        public override void Debug(object message)
        {
            
            base.Debug(message);
            if (base.IsDebugEnabled)
            {
                consoleLog("DEBUG: " + message.ToString());
            }
        }

        public override void Debug(object message, Exception exception)
        {
            base.Debug(message, exception);
            if (base.IsDebugEnabled)
            {
                consoleLog("DEBUG: " + message.ToString());
            }
        }

        public override void Error(object message)
        {
            base.Error(message);
            if (base.IsErrorEnabled)
            {
                consoleLog("ERROR: " + message.ToString());
            }
        }

        public override void Error(object message, Exception exception)
        {
            base.Error(message, exception);
            if (base.IsErrorEnabled)
            {
                consoleLog("ERROR: " + message.ToString());
            }
        }

        public override void Fatal(object message)
        {
            base.Fatal(message);
            if (base.IsFatalEnabled)
            {
                consoleLog("FATAL: " + message.ToString());
            }
        }

        public override void Fatal(object message, Exception exception)
        {
            base.Fatal(message, exception);
            if (base.IsFatalEnabled)
            {
                consoleLog("FATAL: " + message.ToString());
            }
        }

        public override void Info(object message)
        {
            base.Info(message);
            consoleLog(message.ToString());
        }

        public override void Info(object message, Exception exception)
        {
            base.Info(message, exception);
            consoleLog(message.ToString());
        }

        public override void Warn(object message)
        {
            base.Warn(message);
            if (base.IsWarnEnabled)
            {
                consoleLog("WARN: " + message.ToString());
            }
        }

        public override void Warn(object message, Exception exception)
        {
            base.Warn(message, exception);
            if (base.IsWarnEnabled)
            {
                consoleLog("WARN: " + message.ToString());
            }
        }

        private void consoleLog(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}
