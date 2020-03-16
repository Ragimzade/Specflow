using Framework.BaseClasses;
using OpenQA.Selenium;

namespace MailRu.Pages
{
    public class MainPage : BaseForm
    {
        private static readonly By TxtLogin = By.Id("mailbox:login");
        private static readonly By BtnPassword = By.Id("mailbox:submit");
        private static readonly By TxtPassword = By.Id("mailbox:password");
        private static readonly By BtnSearch = By.ClassName("search__category");

        public MainPage() : base(BtnSearch, "Main Page")
        {
        }

        public InboxPage SignIn(string login, string password)
        {
            TypeValue(TxtLogin, login);
            Click(BtnPassword);
            TypeValue(TxtPassword, password);
            Click(BtnPassword);
            return new InboxPage();
        }
    }
}