using MyBotRE.Adapters;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

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
            var distributionName = DistrosParser.GetDistribution(command[1]).Result;
            message = distributionName.GetInfo();
            //await using Stream stream = System.IO.File.OpenRead(distributionName.imagePath);
            await botClient.SendTextMessageAsync(chatId, message);
        }
        public static async Task GetDistrosMessage(ITelegramBotClient botClient, long chatId)
        {
            string msg = "Choose";

            //var lsit = DistrosParser.GetDistrosList();
            //foreach (var item in lsit)
            //{
            //    msg += item + "\n";

            //    if (msg.Length > 4095)
            //    {
            //        break;
            //    }
            //}
            InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(InlineHandler.GetDistroListInline(DistrosParser.GetDistrosList()).Result);

            await botClient.SendTextMessageAsync(chatId, msg,replyMarkup: inlineKeyboard);
        }
    }
}
