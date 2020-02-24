using OpenQA.Selenium;

namespace Framework.Elements
{
    public class TextField : ElementFinder
    {
        private string _typedValue;


        public string TypedValue
        {
            get => _typedValue;
            set => WaitForElement(Locator).SendKeys(_typedValue = value);
        }

        public By Locator { get; set; }
    }
}