using System;
using Framework.Utils;
using NUnit.Framework;

namespace Api.Test
{
    public class DatabaseTest
    {
        private const string Sql = "SELECT * FROM database.products";

        [Test]
        public void TestDb()
        {
            var db = DatabaseUtils.Instance();
            var dict = db.GetQueryMysql(Sql);
            foreach (var delivery in dict)
            {
                foreach (var (key, value) in delivery)
                {
                    Console.WriteLine(key + " : " + value);
                }
            }

            db.CloseConnection();
        }
    }
}