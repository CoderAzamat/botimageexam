namespace TelegramImageBot.Entities;

public class User
{
    public long Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public DateTime JoinedAt { get; set; }

    public override string ToString()
        => $"{Id} | @{Username} | {FirstName} | {JoinedAt:yyyy-MM-dd HH:mm:ss}";
}