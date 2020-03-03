using Framework.BaseClasses;
using OpenQA.Selenium;

namespace Framework.Utils
{
    public class ScreenshotUtils : BaseEntity
    {
        public static string GetScreenshot(string screenshotName = "Screen")
        {
            Log.Error("The test failed and about to grab a screenshot");
            var screenshot = ((ITakesScreenshot) Driver).GetScreenshot();

            if (screenshotName.Equals("Screen"))
            {
                var filename = "./screenshots/" +
                               DateUtils.GetTimeStamp() + "-" + screenshotName + ".png";
                screenshot.SaveAsFile(filename, ScreenshotImageFormat.Png);
                return filename;
            }

            screenshot.SaveAsFile(screenshotName, ScreenshotImageFormat.Png);
            Log.Error("The screen has been taken and stored as " + screenshotName);

            return screenshotName;
        }
    }
}