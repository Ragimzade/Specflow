using Framework.BaseClasses;
using Framework.Utils;
using TechTalk.SpecFlow;

namespace Bdd.Hooks
{
    [Binding]
    public class Hooks : BaseEntity
    {
        [BeforeScenario]
        public void BeforeScenarioSteps()
        {
            Driver = GetDriver(Config.Browser);
            FileUtils.CleanDirectory(FileUtils.BuildDirectoryPath());
        }

        [AfterScenario]
        public void AfterScenarioSteps()
        {
            ScreenshotUtils.TakeScreenshot();
            QuitBrowser();
        }
    }
}