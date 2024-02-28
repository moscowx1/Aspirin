namespace Configurations;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class ConnectionStrings
{
    public string Identity { get; set; }
}

public class Configuration
{
    public ConnectionStrings ConnectionStrings { get; set; }
}
