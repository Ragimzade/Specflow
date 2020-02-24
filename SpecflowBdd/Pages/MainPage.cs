using Framework.BaseClasses;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Bdd.Pages
{
    public class MainPage : BaseForm
    {
        private static readonly By FormMainPage = By.ClassName("general-summary");
        private static readonly By DpdBrand = By.Name("brand_id[]");
        private static readonly By DpdModel = By.Name("model_id[]");
        private static readonly By BtnFind = By.XPath("//button[@class='button button-primary']");
        private static readonly By BtnLogin = By.XPath("//a[@class='nav__link js-open-auth-drawer']");

        public MainPage() : base(FormMainPage, "MainPage")
        {
        }

        public ResultPage FilterCars(string brand, string model = null)
        {
            var dpdBrand = new SelectElement(WaitForElement(DpdBrand));
            dpdBrand.SelectByText(brand);
            var drpModel = new SelectElement(WaitForElement(DpdModel));
            drpModel.SelectByText(model);
            WaitForElement(BtnFind).Click();
            return new ResultPage();
        }

        public LoginPage GoToLoginPage()
        {
            WaitForElementToBeClickable(BtnLogin).Click();
            return new LoginPage();
        }
    }
}