namespace Model;

public class User
{
    public long Id { get; set; }

    public string Login { get; set; }

    public string PasswordHash { get; set; }

    public string PasswordSalt { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private User() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public User(string login, string hash, string salt)
    {
        Login = login;
        PasswordHash = hash;
        PasswordSalt = salt;
    }
}
