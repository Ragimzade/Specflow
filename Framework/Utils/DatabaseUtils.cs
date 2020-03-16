using System;
using System.Collections.Generic;
using System.Data;
using Framework.BaseClasses;
using MySql.Data.MySqlClient;

namespace Framework.Utils
{
    public class DatabaseUtils : BaseEntity
    {
        private const string Host = "localhost";
        private const int Port = 3306;
        private const string Database = "database";
        private const string Username = "root";
        private const string Password = "9802357s";
        private static MySqlConnection _connection = GetConnection();
        private static DatabaseUtils _instance;
        private static readonly object Locker = new object();

        public static DatabaseUtils Connect()
        {
            if (_instance == null)
                lock (Locker)
                {
                    _instance = new DatabaseUtils();
                    _connection.Open();
                }
            return _instance;
        }

        private static MySqlConnection
            GetConnection(string host = Host, int port = Port, string database = Database,
                string username = Username,
                string password = Password)
        {
            var connString = $"Server={host};Database={database};port={port};username={username};password={password}";
            _connection = new MySqlConnection(connString);
            return _connection;
        }

        public IEnumerable<Dictionary<string, object>> GetQueryMysql(string sqlQuery)
        {
            var resultSet = new List<Dictionary<string, object>>();
            try
            {
                var sqlCommand = new MySqlCommand(sqlQuery, _connection);
                var reader = sqlCommand.ExecuteReader();
                var columnCount = reader.FieldCount;
                while (reader.Read())
                {
                    var row = new Dictionary<string, object>(columnCount);
                    for (var i = 0; i < columnCount; ++i)
                    {
                        row.Add(reader.GetName(i), reader.GetValue(i));
                    }
                    resultSet.Add(row);
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
            finally
            {
                Dispose();
            }
            return resultSet;
        }

        private static void Dispose()
        {
            try
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}