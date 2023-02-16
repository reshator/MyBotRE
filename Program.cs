using MyBotRE.Handlers;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class Program
{
    const int maxCount = 8;
    private static async Task Main(string[] args)
    {
        var token = Environment.GetEnvironmentVariable("TOKEN")!;
        var client = new TelegramBotClient(token);
        client.StartReceiving(Update, ErrorHandler);
        Console.ReadLine();
    }
    private static async Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            await (update.Type switch
            {
                UpdateType.Message => BotMessageReceived(botClient, update.Message!),
                UpdateType.InlineQuery => BotInlineQueryReceived(botClient, update.InlineQuery!),
                _ => Task.CompletedTask
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception while handling: {update.Type}: {ex}");
        }
    }

    private static async Task BotInlineQueryReceived(ITelegramBotClient botClient, InlineQuery inlineQuery)
    {
        await Task.CompletedTask;
    }

    private static async Task BotMessageReceived(ITelegramBotClient botClient, Message message)
    {
        try
        {
            await (message.Text! switch
            {
                "/help" => MessageHandler.GetHelp(botClient, message.Chat.Id),
                "/get" => MessageHandler.GetDistros(botClient, message.Chat.Id),
                _ => Task.CompletedTask
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception while handling message: {ex}");
        }
    }

    private static async Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        await Task.Run(() => Console.WriteLine(ErrorMessage));
    }


}