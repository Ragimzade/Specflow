using System.Collections.Generic;
using System.Linq;
using Bdd.Model;
using Bdd.Utils;
using Framework.BaseClasses;
using OpenQA.Selenium;

namespace Bdd.Pages
{
    public class ResultPage : BaseForm
    {
        private static readonly By FormResultPage = By.ClassName("sorting-list");
        private static readonly By LblCarDescription = By.XPath("//div[@class='listing-item-body']");
        private static readonly By LblChildCarName = By.XPath(".//div[@class='listing-item-title']//a");
        private static readonly By LblChildCarPrice = By.XPath(".//strong");
        private static readonly By LblChildCarYear = By.XPath(".//div[@class='listing-item-price']/span");
        private static readonly By LblChildCarDate = By.XPath(".//div[@class='listing-item-date']");
        private static readonly By BtnSortByPrice = By.XPath("//ul[@class='sorting-list']//a[contains(.,'цене')]");
        private static readonly By BtnSortByDate = By.XPath("//ul[@class='sorting-list']//a[contains(.,'дате')]");
        private static readonly By BtnSortByYear = By.XPath("//ul[@class='sorting-list']//a[contains(.,'году')]");
        
        public ResultPage() : base(FormResultPage, "resultPage")
        {
        }

        private CarData GetCarData(ISearchContext car)
        {
            var name = car.FindElement(LblChildCarName).Text;
            var price = car.FindElement(LblChildCarPrice).Text;
            var year = car.FindElement(LblChildCarYear).Text;
            var date = GetCarDate(car);
            return new CarData(name, int.Parse(price.Substring(0, price.Length - 3).Replace(" ", string.Empty)), year,
                DateConverter.ConvertDate(date));
        }

        private string GetCarDate(ISearchContext car)
        {
            try
            {
                var carDate = car.FindElement(LblChildCarDate);
                return carDate.Text;
            }
            catch (NoSuchElementException)
            {
                Log.Debug($"Element{LblChildCarDate} is not found");
                return "";
            }
        }

        public List<CarData> GetCarsOnPage()
        {
            WaitForChildElement(LblCarDescription, LblChildCarName);
            var cars = Driver.FindElements(LblCarDescription);
            return cars.Select(GetCarData).ToList();
        }

        public List<CarData> SortResultByPrice()
        {
            WaitForElementToBeClickable(BtnSortByPrice).Click();
            return GetCarsOnPage();
        }

        public List<CarData> SortResultByYear()
        {
            WaitForElementToBeClickable(BtnSortByYear).Click();
            return GetCarsOnPage();
        }

        public List<CarData> SortResultByDate()
        {
            WaitForElementToBeClickable(BtnSortByDate).Click();
            return GetCarsOnPage();
        }
    }
}