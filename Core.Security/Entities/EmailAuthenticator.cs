using Core.Persistence.Repositories;

namespace Core.Security.Entities;

public class EmailAuthenticator : Entity<int>
{
    public int UserId { get; set; }
    public string? ActivationKey { get; set; }
    public bool IsVerified { get; set; }

    public User User { get; set; } = null!;

    public EmailAuthenticator()
    {
        UserId = default;
        ActivationKey = string.Empty;
        IsVerified = false;
    }

    public EmailAuthenticator(int userId, string secretKey, bool isVerified)
    {
        UserId = userId;
        ActivationKey = secretKey;
        IsVerified = isVerified;
    }

    public EmailAuthenticator(int id, int userId, string secretKey, bool isVerified) : base(id)
    {
        UserId = userId;
        ActivationKey = secretKey;
        IsVerified = isVerified;
    }
}
