using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramImageBot.Services;

namespace TelegramImageBot;

class Program
{
  
    private const string BotToken = "8580286197:AAFkJg3Nc4_q9M3M3bscTjlllKwwRHjwlOY";

    static async Task Main(string[] args)
    {

        var bot = new TelegramBotClient(BotToken);
        var me = await bot.GetMe();
        Console.WriteLine($"Bot: @{me.Username}  |  ID: {me.Id}\n");

        var handler = new BotHandler(bot);
        var cts = new CancellationTokenSource();

        bot.StartReceiving(
            updateHandler: async (b, update, ct) => await handler.HandleUpdateAsync(update, ct),
            errorHandler: async (b, ex, ct) => await handler.HandleErrorAsync(ex, ct),
            receiverOptions: new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            },
            cancellationToken: cts.Token
        );

        Console.WriteLine("Bot ishlamoqda\n");
        Console.ReadLine();

        cts.Cancel();
    }
}