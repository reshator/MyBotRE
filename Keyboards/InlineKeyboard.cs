using Telegram.Bot.Types.ReplyMarkups;

namespace MyBotRE.Keyboards
{
    public class InlineKeyboard
    {
        private static InlineKeyboardButton[][] GetInlineKeyboard(List<string> distrosNames)
        {
            int row = 0;
            var keyboardInline = new InlineKeyboardButton[10][];

            for (int i = 0; i < 8; i++)
            {
                var keyboardButtons2 = new InlineKeyboardButton[1];
                keyboardButtons2[0] = InlineKeyboardButton.WithCallbackData(text: distrosNames[i], callbackData: distrosNames[i]);
                keyboardInline[row] = keyboardButtons2;
                row++;
            }

            var keyboardButtons = new InlineKeyboardButton[2];
            keyboardButtons[0] = InlineKeyboardButton.WithCallbackData(text: "<<", callbackData: "<<");
            keyboardButtons[1] = InlineKeyboardButton.WithCallbackData(text: ">>", callbackData: ">>");
            keyboardInline[9] = keyboardButtons;
            return keyboardInline;
        }
    }
}
