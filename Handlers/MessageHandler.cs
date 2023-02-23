using MyBotRE.Adapters;
using Telegram.Bot;

namespace MyBotRE.Handlers
{
    public static class MessageHandler
    {

        public static async Task GetStart(ITelegramBotClient botClient, long chatId, string username, string? chatName = null)
        {
            new SqlAdapter().AddUserToDB(chatId, username, chatName);
            await botClient.SendTextMessageAsync(chatId,
               "Hello! I'am an arch provider.");
        }

        public static Task GetHelp(ITelegramBotClient botClient, long chatId) =>
            botClient.SendTextMessageAsync(chatId,
                "These commands are supported:\n" +
                "\n/help — list of commands." +
                "\n/get — get distros.");

        public static async Task GetDistros(ITelegramBotClient botClient, long chatId, string? distname)
        {
            string message;
            var command = distname!.Split();
            if (command.Length == 2)
            {
                var distributionName = DistrosParser.GetDistribution(command[1]).Result;
                message = distributionName.GetInfo();

            }
            else
            {
                var distributionName = DistrosParser.GetDistribution(distname).Result;
                message = distributionName.GetInfo();

            }

            await botClient.SendTextMessageAsync(chatId, message);
        }
        public static async Task GetDistrosMessage(ITelegramBotClient botClient, long chatId)
        {
            string msg = string.Empty;

            var lsit = DistrosParser.GetDistrosList();
            foreach (var item in lsit)
            {
                msg += item + "\n";

                if (msg.Length > 4095)
                {
                    break;
                }
            }

            await botClient.SendTextMessageAsync(chatId, msg);
        }
    }
}
