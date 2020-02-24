using System.IO;
using Framework.BaseClasses;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Framework.Utils
{
    public class ScreenshotUtils : BaseEntity
    {
        public static void TakeScreenshot()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                GetScreenshot();
            }
        }

        private static void GetScreenshot()
        {
            Log.Error("The test failed and about to grab a screenshot");
            var filename = Path.Combine(FileUtils.BuildDirectoryPath(),
                DateUtils.GetTimeStamp() + "-" + ScenarioContext.Current.ScenarioInfo.Title + ".png");

            ((ITakesScreenshot) Driver).GetScreenshot()
                .SaveAsFile(filename, ScreenshotImageFormat.Png);
            Log.Error("The screen has been taken and stored as " + filename);
        }
    }
}