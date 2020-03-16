﻿using System;
 using Framework.BaseClasses;
 using OpenQA.Selenium;
 using OpenQA.Selenium.Chrome;
 using OpenQA.Selenium.Edge;
 using OpenQA.Selenium.Firefox;
 using WebDriverManager;
 using WebDriverManager.DriverConfigs.Impl;

 namespace Framework.Browsers
{
    public  class BrowserFactory : BaseEntity
    {
        private BrowserFactory()
        {
        }
        
        public static IWebDriver InitDriver(string browser)
        {
            IWebDriver driver = null;

            switch (browser)
            {
                case "Firefox":
                    var firefoxOptions = BrowserOptions.GetFirefoxOptions();
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver(firefoxOptions);
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.ImplicitWait);
                    break;

                case "Chrome":
                    var chromeOptions = BrowserOptions.GetChromeOptions();
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver(chromeOptions);
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.ImplicitWait);
                    break;

                case "Edge":
                    driver = new EdgeDriver();
                    break;
            }

            return driver;
        }
    }
}