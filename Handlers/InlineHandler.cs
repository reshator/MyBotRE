using Telegram.Bot.Types.ReplyMarkups;

namespace MyBotRE.Handlers
{
    public class InlineHandler
    {
        private static int MaxCount = 8;
        public static async Task<InlineKeyboardButton[][]> GetDistroListInline(List<string> distrosList, int position = 0)
        {
            int buttonsOnScreen = position + MaxCount;
            InlineKeyboardButton[][] inlineKeyboardButtons = new InlineKeyboardButton[MaxCount][];

            await Task.Run(() =>
            {
                for (int i = position; i < buttonsOnScreen; i++)
                {
                    var keyboardOneRow = new InlineKeyboardButton[1];
                    keyboardOneRow[0] = InlineKeyboardButton.WithCallbackData(distrosList[i], distrosList[i]);
                    inlineKeyboardButtons[i] = keyboardOneRow;
                }
                var keyboardButtons = new InlineKeyboardButton[2];
                keyboardButtons[0] = InlineKeyboardButton.WithCallbackData(text: "<<", callbackData: "<<");
                keyboardButtons[1] = InlineKeyboardButton.WithCallbackData(text: ">>", callbackData: ">>");
                inlineKeyboardButtons[MaxCount - 1] = keyboardButtons;

            });

            return inlineKeyboardButtons;
        }

    }
}
