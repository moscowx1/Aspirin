namespace Model;

public class User(string login, string hash, string salt)
{
    public long Id { get; set; }

    public string Login { get; set; } = login;

    public string PasswordHash { get; set; } = hash;

    public string PasswordSalt { get; set; } = salt;
}
