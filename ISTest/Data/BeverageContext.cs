namespace ISTest.Data;

public class BeverageContext : DbContext
{
    public BeverageContext(DbContextOptions<BeverageContext> options) : base(options)
    {
    }

    public DbSet<Beverage> Beverages { get; set; }
    public DbSet<Coin> Coins { get; set; }
    public DbSet<VendingMachine> VendingMachines { get; set; }
    public DbSet<BeverageToVendingMachine> BeverageToVendingMachines { get; set; }
    public DbSet<CoinToVendingMachine> CoinToVendingMachines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BeverageToVendingMachine>(entity =>
        {
            entity.HasKey(e => new { e.BeverageId, e.VendingMachineId });
        });

        modelBuilder.Entity<CoinToVendingMachine>(entity =>
        {
            entity.HasKey(e => new { e.CoinId, e.VendingMachineId });
        });

        modelBuilder.Entity<Coin>().HasData(InitCollections.InitCoinsCollection);
        modelBuilder.Entity<Beverage>().HasData(InitCollections.InitBeveragesCollection);
        modelBuilder.Entity<VendingMachine>().HasData(InitCollections.InitVendingMachineCollection);
        modelBuilder.Entity<BeverageToVendingMachine>().HasData(InitCollections.InitBeverageToVendingMachineCollection);
        modelBuilder.Entity<CoinToVendingMachine>().HasData(InitCollections.InitCoinToVendingMachineCollection);
    }
}

static class InitCollections
{
    public static readonly IEnumerable<Coin> InitCoinsCollection = new Coin[]
    {
        new Coin{ Id=1, Value=1, ImagePath="97(16)_1R_R.gif" },
        new Coin{ Id=2, Value=2, ImagePath="97(16)_2R_R.gif" },
        new Coin{ Id=3, Value=5, ImagePath="97(16)_5R_R.gif" },
        new Coin{ Id=4, Value=10, ImagePath="97(16)_10R_R.gif" }
    };

    public static readonly IEnumerable<Beverage> InitBeveragesCollection = new Beverage[]
    {
        new Beverage{ Id=1, Name="Coca-Cola", Volume=0.33m, Price  =20m },
        new Beverage{ Id=2, Name="Fanta", Volume=0.33m, Price=17m },
        new Beverage{ Id=3, Name="Sprite", Volume=0.33m, Price=15m },
        new Beverage{ Id=4, Name="Merinda", Volume=0.33m, Price=22m },
        new Beverage{ Id=5, Name="BonAqua", Volume=0.5m, Price=23m }
    };

    public static readonly IEnumerable<VendingMachine> InitVendingMachineCollection = new VendingMachine[]
    {
        new VendingMachine
        {
            Id=1,
            Name="Автомат на Уральской"
        }
    };

    public static readonly IEnumerable<BeverageToVendingMachine> InitBeverageToVendingMachineCollection = new BeverageToVendingMachine[]
    {
        new BeverageToVendingMachine { BeverageId=1, VendingMachineId =1, Number=10  },
                new BeverageToVendingMachine { BeverageId=2, VendingMachineId =1, Number=10  },
                new BeverageToVendingMachine { BeverageId=3, VendingMachineId =1, Number=10  },
                new BeverageToVendingMachine { BeverageId=4, VendingMachineId =1, Number=10  }
    };

    public static readonly IEnumerable<CoinToVendingMachine> InitCoinToVendingMachineCollection = new CoinToVendingMachine[]
    {
        new CoinToVendingMachine { VendingMachineId =1, CoinId=1, Amount=100},
        new CoinToVendingMachine { VendingMachineId =1, CoinId=2, Amount=100},
        new CoinToVendingMachine { VendingMachineId =1, CoinId=3, Amount=100},
        new CoinToVendingMachine { VendingMachineId =1, CoinId=4, Amount=100}
    };
}

