using AventStack.ExtentReports;
using Framework.BaseClasses;
using Framework.Browsers;
using Framework.Reporting;
using Framework.Utils;
using MailRu.Pages;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace MailRu.Test
{
    public class BaseTest : BaseEntity
    {
        protected StepHelper StepHelper;
        protected MainPage MainPage;


        [OneTimeTearDown]
        public void TearDown()
        {
            ExtentManager.Instance.Flush();
        }

        [SetUp]
        public void BeforeTest()
        {
            Driver.OpenBaseUrl();
            StepHelper = new StepHelper();
            MainPage = new MainPage();
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                ? ""
                : $"<pre>{TestContext.CurrentContext.Result.StackTrace + TestContext.CurrentContext.Result.Message}</pre>";
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            ExtentTestManager.GetTest().Log(logstatus, "Test ended with " + logstatus + stacktrace);
            if (logstatus == Status.Fail)
            {
                var screenShotPath = ((ITakesScreenshot) Driver).GetScreenshot().AsBase64EncodedString;
                ExtentTestManager.GetTest().Fail("Screenshot -",
                    MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShotPath).Build());
            }

            QuitBrowser();
        }
    }
}