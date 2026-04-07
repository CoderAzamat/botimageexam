using TelegramImageBot.Entities;

namespace TelegramImageBot.Services;

public class UserService
{
    private readonly string _filePath = "users.txt";

    public void Save(User user)
    {
        if (AlreadyExists(user.Id))
        {
            Console.WriteLine($"[UserService] Allaqachon saqlangan: {user.Id}");
            return;
        }

        File.AppendAllText(_filePath, user.ToString() + Environment.NewLine);
        Console.WriteLine($"[UserService] Yangi user saqlandi: {user}");
    }

    private bool AlreadyExists(long userId)
    {
        if (!File.Exists(_filePath)) return false;

        foreach (var line in File.ReadAllLines(_filePath))
            if (line.StartsWith(userId.ToString()))
                return true;

        return false;
    }
}