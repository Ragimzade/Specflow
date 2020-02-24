using Framework.BaseClasses;
using Framework.Elements;
using OpenQA.Selenium;

namespace Bdd.Pages
{
    public class LoginPage : BaseForm
    {
        private static readonly By BtnLogin = By.XPath("//button[@class='button button--action']");
        private static readonly By TxtLogin = By.XPath("//input[@id='login']");
        private static readonly By TxtPassword = By.XPath("//input[@id='password']");

        public LoginPage() : base(BtnLogin, "LoginPage")
        {
        }

        public void Login(string login, string password)
        {
            var inputLogin = new TextField {Locator = TxtLogin, TypedValue = login};
            var inputPassword = new TextField {Locator = TxtPassword, TypedValue = password};
            WaitForElementToBeClickable(BtnLogin).Click();
        }
    }
}