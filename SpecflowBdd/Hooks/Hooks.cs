using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Framework.BaseClasses;
using Framework.Utils;
using TechTalk.SpecFlow;

namespace Bdd.Hooks
{
    [Binding]
    public class Hooks : BaseEntity
    {
        private static ScenarioContext _scenarioContext;
        private static ExtentReports _extentReports;
        private static ExtentHtmlReporter _extentHtmlReporter;
        private static ExtentTest _feature;
        private static ExtentTest _scenario;


        [BeforeTestRun]
        public static void InitializeReport()
        {
            _extentHtmlReporter =
                new ExtentHtmlReporter(Path.Combine(AppContext.BaseDirectory, Config.ScreenshotsFolder, "extent.html"));
            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(_extentHtmlReporter);
            FileUtils.CleanDirectory(FileUtils.BuildDirectoryPath());
        }

        [BeforeFeature]
        public static void BeforeFeatureStart(FeatureContext featureContext)
        {
            if (null != featureContext)
            {
                _feature = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title,
                    featureContext.FeatureInfo.Description);
            }
        }

        [BeforeScenario]
        public static void BeforeScenarioStart(ScenarioContext scenarioContext)
        {
            if (null == scenarioContext) return;
            _scenarioContext = scenarioContext;
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title,
                scenarioContext.ScenarioInfo.Description);
        }

        [AfterStep]
        public void AfterEachStep()
        {
            var scenarioBlock = _scenarioContext.CurrentScenarioBlock;
            switch (scenarioBlock)
            {
                case ScenarioBlock.Given:
                    CreateNode<Given>();
                    break;
                case ScenarioBlock.When:
                    CreateNode<When>();
                    break;
                case ScenarioBlock.Then:
                    CreateNode<Then>();
                    break;
                default:
                    CreateNode<And>();
                    break;
            }
        }

        private void CreateNode<T>() where T : IGherkinFormatterModel
        {
            if (_scenarioContext.TestError != null)
            {
                var screenshotPath = ScreenshotUtils.GetScreenshot();
                _scenario.CreateNode<T>(_scenarioContext.StepContext.StepInfo.Text)
                    .Fail(_scenarioContext.TestError.Message,
                        MediaEntityBuilder
                            .CreateScreenCaptureFromPath(screenshotPath, "Fail Image")
                            .Build());
            }
            else
            {
                _scenario.CreateNode<T>(_scenarioContext.StepContext.StepInfo.Text).Pass("");
            }
        }

        [AfterScenario]
        public void AfterScenarioSteps()
        {
            if (_scenarioContext.TestError != null)
            {
                Log.Error(
                    $"The scenario {_scenarioContext.ScenarioInfo.Title} has finished with test error(s): {_scenarioContext.TestError}");
            }

            QuitBrowser();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extentReports.Flush();
        }
    }
}