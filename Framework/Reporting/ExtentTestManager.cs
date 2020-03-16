using System.Runtime.CompilerServices;
using System.Threading;
using AventStack.ExtentReports;

namespace Framework.Reporting
{
    public class ExtentTestManager
    {
        private static ThreadLocal<ExtentTest> _test;
        private static readonly ExtentReports Extent = ExtentManager.Instance;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest GetTest()
        {
            return _test.Value;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ExtentTest CreateTest(string name, string description = null)
        {
            if (_test == null)
                _test = new ThreadLocal<ExtentTest>();

            var t = Extent.CreateTest(name);
            _test.Value = t;

            return t;
        }
    }
}