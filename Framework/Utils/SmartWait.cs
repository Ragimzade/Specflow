using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Framework.Utils
{
    public static class SmartWait
    {
        public static WebDriverWait GetWait(IWebDriver driver, int timeOutInSecond, int pollingIntervalInMillis)
        {
            var waitTimeout = TimeSpan.FromSeconds(timeOutInSecond);
            var checkInterval = TimeSpan.FromMilliseconds(pollingIntervalInMillis);
            var wait = new WebDriverWait(driver, waitTimeout)
            {
                PollingInterval = checkInterval
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return wait;
        }

        public static T WaitFor<T>(IWebDriver driver, Func<IWebDriver, T> condition, int timeOutInSecond = 0,
            int pollingIntervalInMillis = 0)
        {
            return GetWait(driver, timeOutInSecond, pollingIntervalInMillis).Until(condition);
        }
    }
}