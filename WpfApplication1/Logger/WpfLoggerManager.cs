using OHM.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApplication1.Logger
{
    public class WpfLoggerManager : ILoggerManager
    {

        private TextBox _txt;

        public WpfLoggerManager(TextBox txt)
        {
            _txt = txt;
        }

        public ILogger GetLogger(string repository, Type type)
        {
            //log4net.LogManager.CreateRepository(repository);

            return new WpfLogger(log4net.LogManager.GetLogger(type), _txt);
        }

        public ILogger GetLogger(string repository, string name)
        {
            //log4net.LogManager.CreateRepository(repository);

            return new WpfLogger(log4net.LogManager.GetLogger(name), _txt);
        }
    }
}
