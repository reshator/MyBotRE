using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBotRE.Adapters
{
    public class SqlAdapter
    {
        string connectionString = "Data Source=usersdata.db";
        
        public void GetConnectionToDB()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
            }
        }

        public async Task AddUserToDB(long? userId)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            SqliteCommand command = new SqliteCommand($"INSERT INTO Users(user_id) values ({userId!})", connection);
            command.CommandTimeout = 0;
            await Task.Run(() => Console.WriteLine($"В таблицу Users добавлено объектов: {command.ExecuteNonQuery()}"));

        }
    }
}
