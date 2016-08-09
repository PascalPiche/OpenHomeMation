using log4net;
using System;

namespace OHM.Logger
{
    public class DefaultLogger : ILogger
    {
        #region Private Members

        private ILog _log;

        #endregion

        #region Internal Protected Ctor

        internal protected DefaultLogger(ILog log)
        {
            if (log == null)
            {
                throw new ArgumentNullException("log", "The log arguments must not be null");
            }
            _log = log;
        }

        #endregion

        #region Public Properties

        public bool IsDebugEnabled
        {
            get { return _log.IsDebugEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return _log.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return _log.IsFatalEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return _log.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return _log.IsWarnEnabled; }
        }

        public log4net.Core.ILogger Logger
        {
            get { return _log.Logger; }
        }

        #endregion

        #region Public Methods

        public virtual void Debug(object message, Exception exception)
        {
            _log.Debug(message, exception);
        }

        public virtual void Debug(object message)
        {
            _log.Debug(message);
        }

        public virtual void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.DebugFormat(provider, format, args);
        }

        public virtual void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            _log.DebugFormat(format, arg0, arg1, arg2);
        }

        public virtual void DebugFormat(string format, object arg0, object arg1)
        {
            _log.DebugFormat(format, arg0, arg1);
        }

        public virtual void DebugFormat(string format, object arg0)
        {
            _log.DebugFormat(format, arg0);
        }

        public virtual void DebugFormat(string format, params object[] args)
        {
            _log.DebugFormat(format, args);
        }

        public virtual void Error(object message, Exception exception)
        {
            _log.Error(message, exception);
        }

        public virtual void Error(object message)
        {
            _log.Error(message);
        }

        public virtual void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.ErrorFormat(provider, format, args);
        }

        public virtual void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            _log.ErrorFormat(format, arg0, arg1, arg2);
        }

        public virtual void ErrorFormat(string format, object arg0, object arg1)
        {
            _log.ErrorFormat(format, arg0, arg1);
        }

        public virtual void ErrorFormat(string format, object arg0)
        {
            _log.ErrorFormat(format, arg0);
        }

        public virtual void ErrorFormat(string format, params object[] args)
        {
            _log.ErrorFormat(format, args);
        }

        public virtual void Fatal(object message, Exception exception)
        {
            _log.Fatal(message, exception);
        }

        public virtual void Fatal(object message)
        {
            _log.Fatal(message);
        }

        public virtual void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.FatalFormat(provider, format, args);
        }

        public virtual void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            _log.FatalFormat(format, arg0, arg1, arg2);
        }

        public virtual void FatalFormat(string format, object arg0, object arg1)
        {
            _log.FatalFormat(format, arg0, arg1);
        }

        public virtual void FatalFormat(string format, object arg0)
        {
            _log.FatalFormat(format, arg0);
        }

        public virtual void FatalFormat(string format, params object[] args)
        {
            _log.FatalFormat(format, args);
        }

        public virtual void Info(object message, Exception exception)
        {
            _log.Info(message, exception);
        }

        public virtual void Info(object message)
        {
            _log.Info(message);
        }

        public virtual void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.InfoFormat(provider, format, args);
        }

        public virtual void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            _log.InfoFormat(format, arg0, arg1, arg2);
        }

        public virtual void InfoFormat(string format, object arg0, object arg1)
        {
            _log.InfoFormat(format, arg0, arg1);
        }

        public virtual void InfoFormat(string format, object arg0)
        {
            _log.InfoFormat(format, arg0);
        }

        public virtual void InfoFormat(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
        }

        public virtual void Warn(object message, Exception exception)
        {
            _log.Warn(message, exception);
        }

        public virtual void Warn(object message)
        {
            _log.Warn(message);
        }

        public virtual void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            _log.WarnFormat(provider, format, args);
        }

        public virtual void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            _log.WarnFormat(format, arg0, arg1, arg2);
        }

        public virtual void WarnFormat(string format, object arg0, object arg1)
        {
            _log.WarnFormat(format, arg0, arg1);
        }

        public virtual void WarnFormat(string format, object arg0)
        {
            _log.WarnFormat(format, arg0);
        }

        public virtual void WarnFormat(string format, params object[] args)
        {
            _log.WarnFormat(format, args);
        }

        #endregion
    }
}
