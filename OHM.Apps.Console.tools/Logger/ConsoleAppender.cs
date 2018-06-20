using log4net.Appender;
using log4net.Core;
using System;

namespace OHM.Apps.Console.Tools.Logger
{
    public class AppConsoleOutput : AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            if (this.Layout != null)
            {
                this.Layout.Format(System.Console.Out, loggingEvent);
            }
            else
            {
                System.Console.Out.WriteLine(loggingEvent);
            }
        }
    }
}
