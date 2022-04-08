using ISTest.Data;

namespace ISTest.Services;

public class CoinService : ICoinService
{
    private readonly IDbContextFactory<BeverageContext> _contextFactory;

    public CoinService(IDbContextFactory<BeverageContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Coin>> GetAllCoins()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Coins.ToListAsync();
    }
}