using log4net;
using OHM.Logger;
using System;
using System.Windows.Controls;

namespace WpfApplication1.Logger
{
    public class WpfLogger : DefaultLogger, ILogger
    {
        private TextBox output;
        public WpfLogger(ILog log, TextBox output)
            : base(log)
        {
            this.output = output;
        }

        public override void Debug(object message)
        {
            base.Debug(message);
            wpfLog(message.ToString());
        }

        public override void Debug(object message, Exception exception)
        {
            base.Debug(message, exception);
            wpfLog(message.ToString());
        }

        public override void Error(object message)
        {
            base.Error(message);
            wpfLog(message.ToString());
        }

        public override void Error(object message, Exception exception)
        {
            base.Error(message, exception);
            wpfLog(message.ToString());
        }

        public override void Fatal(object message)
        {
            base.Fatal(message);
            wpfLog(message.ToString());
        }

        public override void Fatal(object message, Exception exception)
        {
            base.Fatal(message, exception);
            wpfLog(message.ToString());
        }

        public override void Info(object message)
        {
            base.Info(message);
            wpfLog(message.ToString());
        }

        public override void Info(object message, Exception exception)
        {
            base.Info(message, exception);
            wpfLog(message.ToString());
        }

        public override void Warn(object message)
        {
            base.Warn(message);
            wpfLog(message.ToString());
        }

        public override void Warn(object message, Exception exception)
        {
            base.Warn(message, exception);
            wpfLog(message.ToString());
        }

        public void wpfLog(string message)
        {
            output.Dispatcher.BeginInvoke(new Action(delegate
            {
                output.AppendText(message + "\n");

            }));

        }
    }
}
