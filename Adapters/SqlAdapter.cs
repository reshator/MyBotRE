using Microsoft.Data.Sqlite;

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

        public async Task AddUserToDB(long? userId, string username)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            SqliteCommand command = new SqliteCommand($"INSERT INTO Users(user_id, username) values ({userId}, '{username}')", connection);
            command.CommandTimeout = 0;
            await Task.Run(() => Console.WriteLine($"В таблицу Users добавлено объектов: {command.ExecuteNonQuery()}"));
        }

        public async Task<SqliteCommand> SelectAllUserId()
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            SqliteCommand command = new SqliteCommand("select user_id, username from Users", connection);
            command.CommandTimeout = 2;
            await Task.Run(() => Console.WriteLine($"Выбрано записей: {command.ExecuteNonQuery()}"));
            return command;
        }
    }
}
