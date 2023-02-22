using Microsoft.Data.Sqlite;

namespace MyBotRE.Adapters
{
    public class SqlAdapter
    {
        string connectionString = Environment.GetEnvironmentVariable("connectionString")!;

        public SqlAdapter()
        {
        }

        public async Task GetConnectionToDB()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();
            }
        }

        public async Task AddUserToDB(long? userId, string username, string chatName)
        {
            using var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();
            if (username is null)
                username = chatName;
            SqliteCommand command =
                new SqliteCommand($"INSERT INTO Users(user_id, username) values ({userId}, '{username}')", connection);
            await Task.Run(() => Console.WriteLine($"В таблицу Users добавлено объектов: {command.ExecuteNonQueryAsync()}"));
        }

        public async Task<SqliteCommand> SelectAllUserId()
        {
            var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync();
            SqliteCommand command = new SqliteCommand("select user_id, username from Users", connection);
            command.CommandTimeout = 0;
            await Task.Run(() => Console.WriteLine($"Выбрано записей: {command.ExecuteNonQueryAsync()}"));
            return command;
        }
    }
}
