using System;
using System.IO;
using NUnit.Framework;

namespace Api.Test
{
    public class DatabaseTest
    {
        private const string Sql = "SELECT * FROM database.products";

        [Test]
        public void TestDb()
        {
            // var db = DatabaseUtils.Instance();
            // var dict = db.GetQueryMysql(Sql);
            // foreach (var delivery in dict)
            // {
            //     foreach (var (key, value) in delivery)
            //     {
            //         Console.WriteLine(key + " : " + value);
            //     }
            // }
            
            // Console.WriteLine(Path.GetFullPath(ScreenshotUtils.GetScreenshot()));
            
            var location = Path.Combine(AppContext.BaseDirectory, "Screens", "123" + "-" +".png");
            
            Console.WriteLine(Path.GetFullPath(location));
            Path.GetRelativePath(location);
            FileInfo f = new FileInfo(location);
            string fullname = f.FullName;
            Console.WriteLine(fullname);

        }
    }
}