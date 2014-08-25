using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Logger;
using Rhino.Mocks;
using log4net;

namespace OHM.Tests
{
    [TestClass]
    public class DefaultLoggerUnitTest
    {
        [TestMethod]
        public void TestConstructorWithNull()
        {
            
            DefaultLogger logger = null;
            try
            {
                logger = new DefaultLogger(null);
                Assert.Fail("No exception was thrown when passing a null log to the logger");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
            
        }

        [TestMethod]
        public void TestConstructor()
        {
            var log = MockRepository.GenerateStub<ILog>();
            var log4netlogger = MockRepository.GenerateStub<log4net.Core.ILogger>();
            log.Stub(x => x.Logger).Return(log4netlogger);
            var logger = new DefaultLogger(log);

            Assert.IsNotNull(logger.Logger);
        }

        [TestMethod]
        public void TestDebug()
        {
            var log = MockRepository.GenerateStub<ILog>();
            var logger = new DefaultLogger(log);
            var message = "message";
            var ex = new Exception("ex");
            var format = "";
            var arg = "";
            String[] args = {""};
            IFormatProvider provider = MockRepository.GenerateStub<IFormatProvider>();
            logger.Debug(message);
            logger.Debug(message, ex);

            logger.DebugFormat(format, arg);
            logger.DebugFormat(format, arg, arg);
            logger.DebugFormat(format, arg, arg, arg);
            logger.DebugFormat(format, args);
            logger.DebugFormat(provider, format, args);

            log.AssertWasCalled(x => x.Debug(message));
            log.AssertWasCalled(x => x.Debug(message, ex));
            log.AssertWasCalled(x => x.DebugFormat(format, arg));
            log.AssertWasCalled(x => x.DebugFormat(format, arg, arg));
            log.AssertWasCalled(x => x.DebugFormat(format, arg, arg, arg));
            log.AssertWasCalled(x => x.DebugFormat(format, args));
            log.AssertWasCalled(x => x.DebugFormat(provider, format, args));

        }

        [TestMethod]
        public void TestInfo()
        {
            var log = MockRepository.GenerateStub<ILog>();
            var logger = new DefaultLogger(log);
            var message = "message";
            var ex = new Exception("ex");
            var format = "";
            var arg = "";
            String[] args = { "" };
            IFormatProvider provider = MockRepository.GenerateStub<IFormatProvider>();
            logger.Info(message);
            logger.Info(message, ex);

            logger.InfoFormat(format, arg);
            logger.InfoFormat(format, arg, arg);
            logger.InfoFormat(format, arg, arg, arg);
            logger.InfoFormat(format, args);
            logger.InfoFormat(provider, format, args);

            log.AssertWasCalled(x => x.Info(message));
            log.AssertWasCalled(x => x.Info(message, ex));
            log.AssertWasCalled(x => x.InfoFormat(format, arg));
            log.AssertWasCalled(x => x.InfoFormat(format, arg, arg));
            log.AssertWasCalled(x => x.InfoFormat(format, arg, arg, arg));
            log.AssertWasCalled(x => x.InfoFormat(format, args));
            log.AssertWasCalled(x => x.InfoFormat(provider, format, args));

        }

        [TestMethod]
        public void TestWarn()
        {
            var log = MockRepository.GenerateStub<ILog>();
            var logger = new DefaultLogger(log);
            var message = "message";
            var ex = new Exception("ex");
            var format = "";
            var arg = "";
            String[] args = { "" };
            IFormatProvider provider = MockRepository.GenerateStub<IFormatProvider>();
            logger.Warn(message);
            logger.Warn(message, ex);

            logger.WarnFormat(format, arg);
            logger.WarnFormat(format, arg, arg);
            logger.WarnFormat(format, arg, arg, arg);
            logger.WarnFormat(format, args);
            logger.WarnFormat(provider, format, args);

            log.AssertWasCalled(x => x.Warn(message));
            log.AssertWasCalled(x => x.Warn(message, ex));
            log.AssertWasCalled(x => x.WarnFormat(format, arg));
            log.AssertWasCalled(x => x.WarnFormat(format, arg, arg));
            log.AssertWasCalled(x => x.WarnFormat(format, arg, arg, arg));
            log.AssertWasCalled(x => x.WarnFormat(format, args));
            log.AssertWasCalled(x => x.WarnFormat(provider, format, args));

        }

        [TestMethod]
        public void TestError()
        {
            var log = MockRepository.GenerateStub<ILog>();
            var logger = new DefaultLogger(log);
            var message = "message";
            var ex = new Exception("ex");
            var format = "";
            var arg = "";
            String[] args = { "" };
            IFormatProvider provider = MockRepository.GenerateStub<IFormatProvider>();
            logger.Error(message);
            logger.Error(message, ex);

            logger.ErrorFormat(format, arg);
            logger.ErrorFormat(format, arg, arg);
            logger.ErrorFormat(format, arg, arg, arg);
            logger.ErrorFormat(format, args);
            logger.ErrorFormat(provider, format, args);

            log.AssertWasCalled(x => x.Error(message));
            log.AssertWasCalled(x => x.Error(message, ex));
            log.AssertWasCalled(x => x.ErrorFormat(format, arg));
            log.AssertWasCalled(x => x.ErrorFormat(format, arg, arg));
            log.AssertWasCalled(x => x.ErrorFormat(format, arg, arg, arg));
            log.AssertWasCalled(x => x.ErrorFormat(format, args));
            log.AssertWasCalled(x => x.ErrorFormat(provider, format, args));

        }

        [TestMethod]
        public void TestFatal()
        {
            var log = MockRepository.GenerateStub<ILog>();
            var logger = new DefaultLogger(log);
            var message = "message";
            var ex = new Exception("ex");
            var format = "";
            var arg = "";
            String[] args = { "" };
            IFormatProvider provider = MockRepository.GenerateStub<IFormatProvider>();
            logger.Fatal(message);
            logger.Fatal(message, ex);

            logger.FatalFormat(format, arg);
            logger.FatalFormat(format, arg, arg);
            logger.FatalFormat(format, arg, arg, arg);
            logger.FatalFormat(format, args);
            logger.FatalFormat(provider, format, args);

            log.AssertWasCalled(x => x.Fatal(message));
            log.AssertWasCalled(x => x.Fatal(message, ex));
            log.AssertWasCalled(x => x.FatalFormat(format, arg));
            log.AssertWasCalled(x => x.FatalFormat(format, arg, arg));
            log.AssertWasCalled(x => x.FatalFormat(format, arg, arg, arg));
            log.AssertWasCalled(x => x.FatalFormat(format, args));
            log.AssertWasCalled(x => x.FatalFormat(provider, format, args));

        }

        [TestMethod]
        public void TestFlags()
        {
            var log = MockRepository.GenerateStub<ILog>();
            var logger = new DefaultLogger(log);

            log.Stub(x => x.IsDebugEnabled).Return(true);
            log.Stub(x => x.IsErrorEnabled).Return(true);
            log.Stub(x => x.IsFatalEnabled).Return(true);
            log.Stub(x => x.IsInfoEnabled).Return(true);
            log.Stub(x => x.IsWarnEnabled).Return(true);

            Assert.IsTrue(logger.IsDebugEnabled);
            Assert.IsTrue(logger.IsErrorEnabled);
            Assert.IsTrue(logger.IsFatalEnabled);
            Assert.IsTrue(logger.IsInfoEnabled);
            Assert.IsTrue(logger.IsWarnEnabled);
        }

        
    }

}
