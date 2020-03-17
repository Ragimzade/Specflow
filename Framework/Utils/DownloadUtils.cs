using System.IO;
using Framework.Elements;

namespace Framework.Utils
{
    public class DownloadUtils : ElementFinder
    {
        public static void CleanDirectory(string directoryLocation)
        {
            foreach (var file in new DirectoryInfo(directoryLocation).GetFiles())
            {
                file.Delete();
            }
        }

        public static bool IsFileDownloaded(string filename)
        {
            var path = Config.BrowserDownloadPath;
            var fileInfo = new FileInfo(path);
            return SmartWait.WaitFor(Driver, d =>
                    File.Exists(path) &&
                    fileInfo.Name == filename &&
                    fileInfo.LastWriteTimeUtc >= DateUtils.GetCurrentDate(),
                Config.DownloadTimeOutInSeconds, Config.PollingIntervalInMillis);
        }
    }
}