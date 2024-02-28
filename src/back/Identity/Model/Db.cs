using Configurations;
using Microsoft.EntityFrameworkCore;

namespace Model;

public class Db(Configuration configuration) : DbContext
{
    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(configuration.ConnectionStrings.Identity);
    }
}
