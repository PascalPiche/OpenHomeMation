using System.ServiceProcess;

namespace OpenHomeMationService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new OpenHomeMationService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
