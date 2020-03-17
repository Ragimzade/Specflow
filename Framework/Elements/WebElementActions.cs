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

        private readonly By _locator;

        public WebElementActions(By locator)
        {
            _locator = locator;
        }

        protected WebElementActions()
        {
        }

        public void Click()
        {
            Wait.Until(ElementDoAction(e => e.Click()));
        }

        public string GetText()
        {
            return Wait.Until(ElementDoAction(e => e.Text));
        }

        public void TypeValue(string value)
        {
            WaitForElementToBeClickable(_locator);
            Wait.Until(ElementDoAction(e => e.SendKeys(value)));
        }

        public string GetAttribute(string attribute)
        {
            return Wait.Until(ElementDoAction(e => e.GetAttribute(attribute)));
        }

        public bool IsElementSelected()
        {
            return Wait.Until(ElementDoAction(e => e.Selected));
        }

        public void ScrollIntoView()
        {
            var jse = (IJavaScriptExecutor) Driver;
            Wait.Until(ElementDoAction(e => jse.ExecuteScript("arguments[0].scrollIntoView(true)", e)));
        }

        public string GetChildTextNodeByIndex(IWebElement element, int childIndex = 0)
        {
            var jse = (IJavaScriptExecutor) Driver;
            return (string) Wait.Until(ElementDoAction(element,
                e => jse.ExecuteScript("return arguments[0].childNodes[arguments[1]].textContent;", e, childIndex)));
        }

        private Func<IWebDriver, T> ElementDoAction<T>(Func<IWebElement, T> function)
        {
            return webDriver =>
            {
                var element = InternalFinder(_locator);

                try
                {
                    return function(element);
                }
                catch (StaleElementReferenceException)
                {
                    Log.Debug(
                        $"StaleElementReferenceException element {_locator} was not found for {TimeOutSeconds} seconds");
                    return default;
                }
                catch (InvalidOperationException)
                {
                    Log.Debug(
                        $"InvalidOperationException element {_locator} was not found for {TimeOutSeconds} seconds");
                    return default;
                }
            };
        }

        private Func<IWebDriver, IWebElement> ElementDoAction(Action<IWebElement> action)
        {
            return webDriver =>
            {
                var element = InternalFinder(_locator);

                try
                {
                    action(element);
                    return element;
                }
                catch (StaleElementReferenceException)
                {
                    Log.Debug(
                        $"StaleElementReferenceException element {_locator} was not found for {TimeOutSeconds} seconds");
                    return null;
                }
                catch (InvalidOperationException)
                {
                    Log.Debug(
                        $"InvalidOperationException element {_locator} was not found for {TimeOutSeconds} seconds");
                    return null;
                }
            };
        }

        private Func<IWebDriver, T> ElementDoAction<T>(IWebElement element, Func<IWebElement, T> function)
        {
            return webDriver =>
            {
                try
                {
                    return function(element);
                }
                catch (StaleElementReferenceException)
                {
                    Log.Debug(
                        $"StaleElementReferenceException element {element} was not found for {TimeOutSeconds} seconds");
                    return default;
                }
                catch (InvalidOperationException)
                {
                    Log.Debug(
                        $"InvalidOperationException element {element} was not found for {TimeOutSeconds} seconds");
                    return default;
                }
            };
        }
    }
}