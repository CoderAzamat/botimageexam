using TelegramImageBot.Handlers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramImageBot.Services;


public class BotHandler
{
    private readonly SartHandler _sartHandler;

    private readonly ImageHandler _imageHandler;

    public BotHandler(ITelegramBotClient bot)
    {
        var userService = new UserService();
        var imageService = new ImageService();

        _sartHandler = new SartHandler(bot, userService);

        _imageHandler = new ImageHandler(bot, imageService);
    }

    public async Task HandleUpdateAsync(Update update, CancellationToken ct)
    {
        if (update.Message is not { } message) return;
        if (message.Text is not { } text) return;

        string username = message.From?.Username ?? "unknown";
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] @{username}: {text}");

        if (text.StartsWith("/start"))
        {
            await _sartHandler.HandleAsync(message, ct);
            return;
        }
        await _imageHandler.HandleImageRequestAsync(message, ct);
    }

    public Task HandleErrorAsync(Exception ex, CancellationToken ct)
    {
        Console.WriteLine($"[BotHandler] XATO: {ex.GetType().Name}: {ex.Message}");
        return Task.CompletedTask;
    }
}