using log4net.Appender;
using log4net.Core;
using System;

namespace ConsoleApplication1.Logger
{
    class AppConsoleOutput : AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            if (this.Layout != null)
            {
                this.Layout.Format(Console.Out, loggingEvent);
            }
            else
            {
                Console.Out.WriteLine(loggingEvent);
            }
        }
    }
}
