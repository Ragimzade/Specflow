using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Framework.Utils
{
    public class DatabaseUtils
    {
        private const string Host = "localhost";
        private const int Port = 3306;
        private const string Database = "database";
        private const string Username = "root";
        private const string Password = "9802357s";

        private DatabaseUtils()
        {
        }

        private static DatabaseUtils _instance;

        public static DatabaseUtils Instance()
        {
            return _instance ?? (_instance = new DatabaseUtils());
        }

        private MySqlConnection
            GetConnection(string host = Host, int port = Port, string database = Database, string username = Username, string password = Password)
        {
            var connString = "Server=" + host + ";Database=" + database
                             + ";port=" + port + ";User Id=" + username + ";password=" + password;
            var connection = new MySqlConnection(connString);

            return connection;
        }

        public IEnumerable<Dictionary<string, object>> GetQueryMysql(string sqlQuery)
        {
            var conn = GetConnection();
            conn.Open();
            var sqlCommand = new MySqlCommand(sqlQuery, conn);
            var reader = sqlCommand.ExecuteReader();
            var columnCount = reader.FieldCount;
            var resultSet = new List<Dictionary<string, object>>();
            while (reader.Read())
            {
                var row = new Dictionary<string, object>(columnCount);
                for (var i = 0; i < columnCount; ++i)
                {
                    row.Add(reader.GetName(i), reader.GetValue(i));
                }

                resultSet.Add(row);
            }

            return resultSet;
        }
    }
}