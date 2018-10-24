using log4net.Appender;
using log4net.Core;

namespace OHM.Apps.Console.Tools.Logger
{
    /// <summary>
    /// Redirect Logging event to System.Console.Out with configured layout
    /// </summary>
    public class AppConsoleOutput : AppenderSkeleton
    {
        /// <summary>
        /// Append a loggingEvent to the console output
        /// </summary>
        /// <param name="loggingEvent">Logging event to append</param>
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
