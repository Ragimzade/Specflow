﻿using System;
using System.IO;
using Framework.BaseClasses;

namespace Framework.Utils
{
    public class FileUtils : BaseEntity
    {
        public static string BuildDirectoryPath()
        {
            var location = Path.Combine(AppContext.BaseDirectory, Config.ScreenshotsFolder);
            if (!Directory.Exists(location))
            {
                Directory.CreateDirectory(location);
            }
            return location;
        }

        public static void CleanDirectory(string directoryLocation)
        {
            foreach (var file in new DirectoryInfo(directoryLocation).GetFiles())
            {
                file.Delete();
            }
        }
    }
}