using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;

namespace Framework.Reporting
{
    public class ExtentManager
    {
        private static readonly Lazy<ExtentReports> Lazy = new Lazy<ExtentReports>(() => new ExtentReports());
        public static ExtentReports Instance => Lazy.Value;

        static ExtentManager()
        {
            var htmlReporter = new ExtentHtmlReporter(TestContext.CurrentContext.TestDirectory + "\\Extent.html");
            Instance.AttachReporter(htmlReporter);
        }

        private ExtentManager()
        {
        }
    }
}