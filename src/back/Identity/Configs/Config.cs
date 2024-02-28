namespace Configs;

public record ConnectionStrings(string Identity);

public record Config(ConnectionStrings ConnectionStrings);
