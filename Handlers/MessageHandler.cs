using MyBotRE.Adapters;
using Telegram.Bot;

namespace MyBotRE.Handlers
{
    public static class MessageHandler
    {

        public static Task GetStart(ITelegramBotClient botClient, long chatId)
        {
            new SqlAdapter().AddUserToDB(chatId);
            return botClient.SendTextMessageAsync(chatId,
               "Hello! This is OS searcher. Type /help to know more");
        }
           
        public static Task GetHelp(ITelegramBotClient botClient, long chatId) =>
            botClient.SendTextMessageAsync(chatId,
                "These commands are supported:\n" +
                "\n/help — list of commands." +
                "\n/get — get distros.");

        public static Task GetDistros(ITelegramBotClient botClient, long chatId)
        {
            var list = DistrosParser.GetDistrosList();
            foreach (var distro in list)
            {

            }
            return botClient.SendTextMessageAsync(chatId, "a");
        }
        public static Task GetDistrosMessage(ITelegramBotClient botClient, long chatId)
        {
            string msg = string.Empty;
            
            //var lsit = DistrosParser.GetDistrosList();
            //foreach (var item in lsit)
            //{
            //    msg += item + "\n";

            //    if (msg.Length > 4095)
            //    {
            //        break;
            //    }
            //}

            return botClient.SendTextMessageAsync(chatId, "add");
        }
    }
}
