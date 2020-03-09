using Framework.Browsers;
using Framework.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Framework.BaseClasses
{
    public abstract class BaseForm : WebElementActions
    {
        private readonly By _formSelector;
        private readonly string _formName;
        protected BaseForm(By formSelector, string formName)
        {
            _formSelector = formSelector;
            _formName = $"{formName} form";
            Driver.WaitForPageLoaded();
            AssertFormIsOpened(formSelector);
        }
        private void AssertFormIsOpened(By formSelector)
        {
            if (IsElementPresent(formSelector))
            {
                Log.Info($"Page {_formName} is opened ");
            }
            else
            {
                throw new AssertFailedException($"Form {_formName} is not opened");
            }
        }
        
    }
}