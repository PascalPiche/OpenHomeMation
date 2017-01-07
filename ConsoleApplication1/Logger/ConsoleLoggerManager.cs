﻿using log4net;
using OHM.Logger;
using System;

namespace ConsoleApplication1.Logger
{
    public class ConsoleLoggerManager : ILoggerManager
    {

        public ConsoleLoggerManager()
        {
            
        }

        public ILogger GetLogger(Type type)
        {
            return new ConsoleLogger(LogManager.GetLogger(type));
        }

        public ILogger GetLogger(string name)
        {
            return new ConsoleLogger(LogManager.GetLogger(name));
        }

        public ILogger GetLogger(string repository, Type type)
        {
            throw new NotImplementedException();
        }

        public ILogger GetLogger(string repository, string name)
        {
            throw new NotImplementedException();
        }
    }
}
