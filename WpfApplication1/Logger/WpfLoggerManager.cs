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

        public ILogger GetLogger(Type type)
        {
            return new WpfLogger(log4net.LogManager.GetLogger(type), _txt);
        }

        public ILogger GetLogger(string name)
        {
            return new WpfLogger(log4net.LogManager.GetLogger(name), _txt);
        }
    }
}
