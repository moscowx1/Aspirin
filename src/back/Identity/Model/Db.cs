using Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Model;

public class Db(IOptions<Configuration> configuration) : DbContext
{
    private readonly Configuration _configuration = configuration.Value;

    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_configuration.ConnectionStrings.Identity);
    }
}
