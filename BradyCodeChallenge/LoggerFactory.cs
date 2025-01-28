using log4net.Config;
using log4net;
using System.Reflection;

namespace BradyCodeChallenge
{
    public static class LoggerFactory
    {
        private static readonly ILog log;

        static LoggerFactory()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.xml"));
            log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static ILog GetLogger()
        {
            return log;
        }
    }
}
