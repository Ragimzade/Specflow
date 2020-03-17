using System;
using System.Collections.Generic;
using System.Linq;
using Framework.BaseClasses;
using Framework.Utils;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Framework.Elements
{
    public class ElementFinder : BaseEntity
    {
        private static readonly int TimeOutSeconds = Config.TimeOutInSeconds;
        private static readonly int TimeOutInMillis = Config.PollingIntervalInMillis;

        protected WebElementActions GetElement(By locator)
        {
            return new WebElementActions(locator);
        }
        

        protected IWebElement InternalFinder(By locator)
        {
            try
            {
                return SmartWait.WaitFor(Driver, ElementToBeEnabled(locator),
                    TimeOutSeconds, TimeOutInMillis);
            }
            catch (TimeoutException)
            {
                throw new Exception(
                    $"WebDriverTimeoutException: Element {locator} was not found for {TimeOutSeconds} seconds");
            }
            catch (NoSuchElementException)
            {
                throw new Exception(
                    $"WebDriverTimeoutException: Element {locator} was not found for {TimeOutSeconds} seconds");
            }
        }

        protected IEnumerable<IWebElement> FindElements(By locator)
        {
            WaitForCondition(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
            return Driver.FindElements(locator);
        }

        protected IWebElement FindElementByText(By locator, string text)
        {
            var webElements = FindElements(locator);
            return webElements.FirstOrDefault(e => e.Text.Equals(text));
        }

        protected IWebElement WaitForElement(By locator)
        {
            return InternalFinder(locator);
        }

        protected IWebElement FindChildren(By parentLocator, By childLocator)
        {
            return WaitForElement(parentLocator).FindElements(childLocator).ElementAt(0);
        }

        protected void WaitForChildElement(By parentLocator, By childLocator)
        {
            WaitForElement(parentLocator).FindElement(childLocator);
        }

        private void WaitForCondition<T>(Func<IWebDriver, T> condition)
        {
            SmartWait.WaitFor(Driver, condition, TimeOutSeconds, TimeOutInMillis);
        }

        protected IWebElement FindElement(By locator)
        {
            return InternalFinder(locator);
        }

        protected bool IsElementPresent(By locator)
        {
            try
            {
                WaitForCondition(ExpectedConditions.ElementIsVisible(locator));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void WaitForPresent(By locator)
        {
            WaitForCondition(ExpectedConditions.ElementExists(locator));
        }

        private static Func<IWebDriver, IWebElement> ElementToBeEnabled(By locator)
        {
            var element = Driver.FindElement(locator);
            return webDriver =>
            {
                try
                {
                    return element != null && element.Enabled ? element : (IWebElement) null;
                }
                catch (StaleElementReferenceException)
                {
                    return (IWebElement) null;
                }
            };
        }

        protected IWebElement WaitForElementToBeClickable(By locator)
        {
            WaitForCondition(ExpectedConditions.ElementToBeClickable(locator));
            return InternalFinder(locator);
        }
    }
}