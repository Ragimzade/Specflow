using System;
using System.Linq;
using Bdd.Model;
using Bdd.Pages;
using Framework.Assertions;
using Framework.BaseClasses;
using Framework.Browsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Bdd.StepDefinition
{
    [Binding]
    public class SearchForCarsSteps : BaseEntity
    {
        private ResultPage _resultPage;
        private readonly SoftAssertions _softAssert = new SoftAssertions();
        private readonly CarLists _carLists;

        public SearchForCarsSteps(CarLists carLists)
        {
            _carLists = carLists;
        }

        [Given(@"User is on main page")]
        public void GoToMainPage()
        {
            Driver.OpenBaseUrl();
        }

        [When(@"User search for cars with brand (.*) and model (.*)")]
        public void FindCars(string brand, string model)
        {
            var mainPage = new MainPage();
            _resultPage = mainPage.FilterCars(brand, model);
        }

        [Then(@"user gets result with correct car's brand (.*) and model (.*)")]
        public void AssertResultContainsCorrectCars(string brand, string model)
        {
            _carLists.CarsOnPage = _resultPage.GetCarsOnPage();
            foreach (var car in _carLists.CarsOnPage) Assert.IsTrue(car.Name.Contains(brand + " " + model));
        }

        [When(@"User sorts cars by price")]
        public void SortResultByPrice()
        {
            _carLists.SortedByPrice = _resultPage.SortResultByPrice();
        }

        [Then(@"User sees that cars are sorted by price")]
        public void AssertCarsSortingByPrice()
        {
            var expectedSortingByPrice =
                _carLists.CarsOnPage.OrderBy(car => car.Price).ToList();
            _softAssert.True("Cars are not sorted correctly by price",
                expectedSortingByPrice.SequenceEqual(_carLists.SortedByPrice));
        }

        [When(@"User sorts cars by year")]
        public void SortResultByYear()
        {
            _carLists.SortedByYear = _resultPage.SortResultByYear();
        }

        [Then(@"User sees that cars are sorted by year")]
        public void AssertCarsSortingByYear()
        {
            var expectedSortingByYear = _carLists.SortedByYear.OrderByDescending(car => car.Year);
            _softAssert.True("Cars are not sorted correctly by year",
                expectedSortingByYear.SequenceEqual(_carLists.SortedByYear));
        }

        [When(@"User sorts cars by date")]
        public void SortResultByDate()
        {
            _carLists.SortedByDate = _resultPage.SortResultByDate();
        }

        [Then(@"User sees  that cars are sorted by date")]
        public void AssertCarsSortingByDate()
        {
            var expectedSortingByDate = _carLists.SortedByDate.OrderByDescending(car =>
                {
                    DateTime.TryParse(car.Date, out var date);
                    return date;
                })
                .ToList();
            _softAssert.True("Cars are not sorted correctly by Date",
                expectedSortingByDate.SequenceEqual(_carLists.SortedByDate));
            _softAssert.AssertAll();
        }

        [When(@"User logins with login (.*) and password (.*)")]
        public void Login(string login, string password)
        {
            var mainPage = new MainPage();
            var loginPage = mainPage.GoToLoginPage();
            loginPage.Login(login, password);
        }
    }
}