using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NLog;

namespace Framework.Logging
{
    public class Logg
    {
        private static Logg _instance;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        public static Logg GetInstance()
        {
            return _instance ?? (_instance = new Logg());
        }

        private Logg()
        {
            LogManager.LoadConfiguration("NLog.config");
        }

        public void Debug(string message, Exception exception = null)
        {
            Log.Debug(exception, message);
        }

        public void Info(string message)
        {
            Log.Info(message);
        }

        public void Warn(string message)
        {
            Log.Warn(message);
        }

        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Fatal(string message, Exception exception)
        {
            Log.Fatal(exception, message);
        }

        public void Step(int step, string msg)
        {
            Info($"----==[{step}. {msg} ]==----");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);
            return sf.GetMethod().Name;
        }

        public void Info(int message)
        {
            Log.Info(message);
        }
    }
}