namespace Framework.Configuration
{
    public interface IConfiguration
    {
        string Browser { get; set; }
        int DownloadTimeOutInSeconds { get; set; }
        int TimeOutInSeconds { get; set; }
        int PollingIntervalInMillis { get; set; }
        int PageLoadTimeOutInSeconds { get; set; }
        int ImplicitWait { get; set; }
        string BrowserDownloadPath { get; set; }
        string BaseUrl { get; set; }
        string ScreenshotsFolder { get; set; }
        string MailRuLogin { get; set; }
        string MailRuPassword { get; set; }
    }
}