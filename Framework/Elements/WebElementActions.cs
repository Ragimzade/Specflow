using System;
using Framework.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Framework.Elements
{
    public class WebElementActions : ElementFinder
    {
        private const int TimeOutSeconds = 5;
        private const int TimeOutInMillis = 1000;
        private static readonly WebDriverWait Wait = SmartWait.GetWait(Driver, TimeOutSeconds, TimeOutInMillis);

        protected void Click(By locator)
        {
            Wait.Until(ElementDoAction(locator, e => e.Click()));
        }

        protected string GetText(By locator)
        {
            return Wait.Until(ElementDoAction(locator, e => e.Text));
        }

        protected void TypeValue(By locator, string value)
        {
            Wait.Until(ElementDoAction(locator, e => e.SendKeys(value)));
        }

        protected string GetAttribute(By locator, string attribute)
        {
            return Wait.Until(ElementDoAction(locator, e => e.GetAttribute(attribute)));
        }

        protected bool IsElementSelected(By locator)
        {
            return Wait.Until(ElementDoAction(locator, e => e.Selected));
        }

        protected void ScrollIntoView(By locator)
        {
            var jse = (IJavaScriptExecutor) Driver;
            Wait.Until(ElementDoAction(locator, e => jse.ExecuteScript("arguments[0].scrollIntoView(true)", e)));
        }

        private Func<IWebDriver, T> ElementDoAction<T>(By locator, Func<IWebElement, T> function)
        {
            return webDriver =>
            {
                var element = InternalFinder(locator);

                try
                {
                    return function(element);
                }
                catch (StaleElementReferenceException)
                {
                    Log.Debug(
                        $"StaleElementReferenceException element {locator} was not found for {TimeOutSeconds} seconds");
                    return default;
                }
                catch (InvalidOperationException)
                {
                    Log.Debug(
                        $"InvalidOperationException element {locator} was not found for {TimeOutSeconds} seconds");
                    return default;
                }
            };
        }

        private Func<IWebDriver, IWebElement> ElementDoAction(By locator, Action<IWebElement> action)
        {
            return webDriver =>
            {
                var element = InternalFinder(locator);

                try
                {
                    action(element);
                    return element;
                }
                catch (StaleElementReferenceException)
                {
                    Log.Debug(
                        $"StaleElementReferenceException element {locator} was not found for {TimeOutSeconds} seconds");
                    return null;
                }
                catch (InvalidOperationException)
                {
                    Log.Debug(
                        $"InvalidOperationException element {locator} was not found for {TimeOutSeconds} seconds");
                    return null;
                }
            };
        }
    }
}