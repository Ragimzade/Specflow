using System;
using Framework.Utils;
using NUnit.Framework;

namespace Api.Test
{
    public class DatabaseTest : RestApiTests
    {
        private static DatabaseUtils _db;
        private const string Sql = "SELECT * FROM database.products";

        [Test]
        public void TestDb()
        {
            _db = DatabaseUtils.Connect();
            var dict = _db.GetQueryMysql(Sql);
            foreach (var delivery in dict)
            {
                foreach (var (key, value) in delivery)
                {
                    Console.WriteLine($"{key} : {value}");
                }
            }
        }
    }
}