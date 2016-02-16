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
            consoleLog(message.ToString());
        }

        public override void Debug(object message, Exception exception)
        {
            base.Debug(message, exception);
            consoleLog(message.ToString());
        }

        public override void Error(object message)
        {
            base.Error(message);
            consoleLog(message.ToString());
        }

        public override void Error(object message, Exception exception)
        {
            base.Error(message, exception);
            consoleLog(message.ToString());
        }

        public override void Fatal(object message)
        {
            base.Fatal(message);
            consoleLog(message.ToString());
        }

        public override void Fatal(object message, Exception exception)
        {
            base.Fatal(message, exception);
            consoleLog(message.ToString());
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
            consoleLog(message.ToString());
        }

        public override void Warn(object message, Exception exception)
        {
            base.Warn(message, exception);
            consoleLog(message.ToString());
        }

        public void consoleLog(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}
