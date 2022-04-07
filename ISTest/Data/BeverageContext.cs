namespace ISTest.Data;

public class BeverageContext : DbContext
{
    public BeverageContext(DbContextOptions<BeverageContext> options) : base(options)
    {
    }

    public DbSet<Beverage> Beverages { get; set; }
    public DbSet<Coin> Coins { get; set; }
}
