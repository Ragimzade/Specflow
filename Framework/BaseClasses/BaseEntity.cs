using System;
using System.IO;
using Framework.Browsers;
using Framework.Logging;
using OpenQA.Selenium;

namespace Framework.BaseClasses
{
    public class BaseEntity
    {
        protected static readonly Logg Log = Logg.GetInstance();
        private const string ConfigFileName = "config.json";
        private static IWebDriver _instance;
        private static readonly object Locker = new object();
        protected static IWebDriver Driver => GetDriver;

        protected static readonly Configuration.Configuration Config =
            Configuration.Configuration.ParseConfiguration<Configuration.Configuration>(
                File.ReadAllText(Path.Combine(AppContext.BaseDirectory, ConfigFileName)));
        
        public static IWebDriver GetDriver
        {
            get
            {
                if (_instance != null) 
                    return _instance;
                lock (Locker)
                {
                    _instance = BrowserFactory.InitDriver(Config.Browser);
                }
                return _instance;
            }
        }

        protected static void QuitBrowser()
        {
            if (_instance == null) 
            {return;}
            _instance.Quit();
            _instance = null;
        }
    }
}