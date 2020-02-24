using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Framework.Utils
{
    public static class JsonReader
    {
        private const string ConfigFileName = "config.json";

        private static string ReadValueFromConfig(string value)
        {
            var sr = new StreamReader(ConfigFileName);
            var reader = new JsonTextReader(sr);
            var jObject = JObject.Load(reader);

            return jObject.GetValue(value).Value<string>();
        }

        public static int GetTimeoutInSeconds()
        {
            var timeout = int.Parse(ReadValueFromConfig("timeoutInSeconds"));
            return timeout;
        }

        public static int GetPollingIntervalInMillis()
        {
            var interval = int.Parse(ReadValueFromConfig("pollingIntervalInMillis"));
            return interval;
        }

        public static int GetPageLoadTimeOutInSeconds()
        {
            var interval = int.Parse(ReadValueFromConfig("pageLoadTimeOutInSeconds"));
            return interval;
        }

        public static int GetImplicitWait()
        {
            var implicitWait = int.Parse(ReadValueFromConfig("timeoutImplicitInSeconds"));
            return implicitWait;
        }

        public static string GetBrowserDownloadPath()
        {
            return ReadValueFromConfig("browserDownloadFolder");
        }

        public static string GetBaseUrl()
        {
            return ReadValueFromConfig("baseUrl");
        }

        public static string GetBrowser()
        {
            return ReadValueFromConfig("browser");
        }
    }
}