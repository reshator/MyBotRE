using MyBotRE.Adapters;
using Telegram.Bot;

namespace MyBotRE.Utils
{
    internal class Sender
    {
        public static async Task SendMessageToEveryone(ITelegramBotClient botClient)
        {
            var selection = new SqlAdapter().SelectAllUserId().Result;
            using var sqlReader = selection.ExecuteReader();
            while (sqlReader.Read())   // построчно считываем данные
            {
                var id = sqlReader.GetInt64(0);
                var username = sqlReader.GetString(1);

                await botClient.SendTextMessageAsync(id, $"Дорогой @{username}, " + File.ReadAllText(@"Utils/Message.txt"));
            }
        }
    }
}
