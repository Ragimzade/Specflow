﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Framework.Utils
{
    public static class SmartWait
    {
        public static T WaitFor<T>(IWebDriver driver, Func<IWebDriver, T> condition, int timeOutInSecond = 0,
            int pollingIntervalInMillis = 0)
        {
            var waitTimeout = TimeSpan.FromSeconds(timeOutInSecond);
            var checkInterval = TimeSpan.FromMilliseconds(pollingIntervalInMillis);
            var wait = new WebDriverWait(driver, waitTimeout)
            {
                PollingInterval = checkInterval
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            var result = wait.Until(condition);
            return result;
        }
    }
}