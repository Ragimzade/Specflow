using Newtonsoft.Json;

namespace Framework.Configuration
{
    public class Configuration : IConfiguration
    {
        [JsonProperty("browser")] public string Browser { get; set; }

        [JsonProperty("timeoutInSeconds")] public int TimeOutInSeconds { get; set; }

        [JsonProperty("pollingIntervalInMillis")]
        public int PollingIntervalInMillis { get; set; }

        [JsonProperty("pageLoadTimeOutInSeconds")]
        public int PageLoadTimeOutInSeconds { get; set; }

        [JsonProperty("timeoutImplicitInSeconds")]
        public int ImplicitWait { get; set; }

        [JsonProperty("browserDownloadFolder")]
        public string BrowserDownloadPath { get; set; }

        [JsonProperty("baseUrl")] 
        public string BaseUrl { get; set; }

        [JsonProperty("screenshotsFolder")] 
        public string ScreenshotsFolder { get; set; }
        
        public static T ParseConfiguration<T>(string jsonText) where T : IConfiguration
        {
            return JsonConvert.DeserializeObject<T>(jsonText, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Populate
            });
        }
    }
}