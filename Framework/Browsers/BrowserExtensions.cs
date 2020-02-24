﻿using Framework.Logging;
using Framework.Utils;
using OpenQA.Selenium;

namespace Framework.Browsers
{
    public static class BrowserExtensions
    {
        private static readonly Logg Log = Logg.GetInstance();

        public static string GetCurrentUrl(this IWebDriver driver)
        {
            return driver.Url;
        }

        public static void OpenBaseUrl(this IWebDriver driver)
        {
            Log.Debug("browser");
            driver.Url = JsonReader.GetBaseUrl();
        }

        public static void WaitForPageLoaded(this IWebDriver driver)
        {
            SmartWait.WaitFor(driver,
                b => ((IJavaScriptExecutor) b).ExecuteScript("return document.readyState").Equals("complete"),
                JsonReader.GetPageLoadTimeOutInSeconds());
        }

        public static void RefreshPage(this IWebDriver driver)
        {
            driver.Navigate().Refresh();
        }

        public static int GetWindowSize(this IWebDriver driver)
        {
            return driver.Manage().Window.Size.Height;
        }

        public static void ScrollToElement(this IWebDriver driver, IWebElement webElement)
        {
            var jse = (IJavaScriptExecutor) driver;
            jse.ExecuteScript("arguments[0].scrollIntoView(true)", webElement);
        }

        public static void ScrollToMiddle(this IWebDriver driver)
        {
            var jse = (IJavaScriptExecutor) driver;
            jse.ExecuteScript($"window.scrollBy(0, {GetWindowSize(driver) / 2})");
        }
    }
}