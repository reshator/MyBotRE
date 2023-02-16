using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyBotRE.Handlers
{
    public static class MessageHandler
    {
        public static Task GetHelp(ITelegramBotClient botClient, long chatId) =>
            botClient.SendTextMessageAsync(chatId, 
                "These commands are supported:\n" +
                "\n/help — list of commands." +
                "\n/get — get distros.");

        public static Task GetDistros(ITelegramBotClient botClient, long chatId)
        {
            return botClient.SendTextMessageAsync(chatId, "still rajik");
        }
    }
}
