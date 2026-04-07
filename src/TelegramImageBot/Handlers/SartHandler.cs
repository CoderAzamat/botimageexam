using TelegramImageBot.Entities;
using TelegramImageBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramImageBot.Handlers;

public class SartHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly UserService _userService;

    public SartHandler(ITelegramBotClient bot, UserService userService)
    {
        _bot = bot;
        _userService = userService;
    }

    public async Task HandleAsync(Message message, CancellationToken ct)
    {
        long chatId = message.Chat.Id;
        long userId = message.From!.Id;
        string username = message.From.Username ?? "";
        string firstName = message.From.FirstName ?? "User";

        _userService.Save(new TelegramImageBot.Entities.User
        {
            Id = userId,
            Username = username,
            FirstName = firstName,
            JoinedAt = DateTime.Now
        });

        await _bot.SendMessage(
            chatId,
            $"👋 Salom, <b>{firstName}</b>!\n\n" +
            "🖼 Men rasm qidiruvchi botman.\n\n" +
            "Istalgan so'z yozing → <b>3 ta rasm</b> topib beraman!\n\n" ,
            parseMode: ParseMode.Html,
            cancellationToken: ct);

        Console.WriteLine($"[SartHandler] /start → @{username} ({userId})");
    }
}