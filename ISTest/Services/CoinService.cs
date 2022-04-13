using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISTest.Data;

namespace ISTest.Services;

public class CoinService : ICoinService
{
    private readonly IDbContextFactory<BeverageContext> _contextFactory;
    private readonly IMapper _mapper;

    public CoinService(IDbContextFactory<BeverageContext> contextFactory, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<ICollection<CoinDto>> GetAllCoins()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Coins.ProjectTo<CoinDto>(_mapper.ConfigurationProvider).OrderBy(x => x.Value).ToListAsync();
    }

    public async Task<ICollection<CoinToVendingMachineDto>> GetAllCoins(int vendingMachineId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Coins.Where(x => x.CoinToVendingMachines.Any(x => x.VendingMachineId == vendingMachineId))
            .ProjectTo<CoinToVendingMachineDto>(_mapper.ConfigurationProvider).ToListAsync();

        //return await context.Beverages.Where(x => x.BeverageToVendingMachines.Any(x => x.VendingMachineId == vendingMachineId))
        //    .ProjectTo<BeverageForVendingMachineDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task AddCoinToVendingMachine(int vendingMachineId, int coinId)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.CoinToVendingMachines.FirstOrDefaultAsync(x => x.VendingMachineId == vendingMachineId && x.CoinId == coinId);
        if (entity is not null)
            entity.Amount++;
        else
        {
            await context.CoinToVendingMachines.AddAsync(new CoinToVendingMachine
            {
                VendingMachineId = vendingMachineId,
                CoinId = coinId,
                Amount = 1
            });
        }
        await context.SaveChangesAsync();
    }

    public async Task<ICollection<(int coinId, int amount)>> GetChangeFromVendingMachine(int vendingMachineId, decimal bank)
    {
        Dictionary<int, int> changeBank = new();

        using var context = _contextFactory.CreateDbContext();
        var coins = await context.CoinToVendingMachines.Where(x => x.VendingMachineId == vendingMachineId)
            .Include(x => x.Coin).OrderByDescending(x => x.Coin.Value).ToListAsync();
        foreach (var coinVm in coins)
        {
            while ((bank - coinVm.Coin.Value >= 0) && coinVm.Amount > 0)
            {
                if (changeBank.ContainsKey(coinVm.Coin.Id)) changeBank[coinVm.Coin.Id]++;
                else changeBank[coinVm.Coin.Id] = 1;
                coinVm.Amount--;
                bank -= coinVm.Coin.Value;
            }
        }
        await context.SaveChangesAsync();
        return changeBank.Select(x => (x.Key, x.Value)).ToArray();
    }

    public async Task UpdateVendingMachineCoins(int vendingMachineId, IEnumerable<CoinToVendingMachineDto> coins)
    {
        using var context = _contextFactory.CreateDbContext();
        var vendingMachine = await context.VendingMachines.Include(x => x.CoinToVendingMachines).FirstOrDefaultAsync(x => x.Id == vendingMachineId);
        if (vendingMachine is null) return;

        var removeItems = vendingMachine.CoinToVendingMachines.Where(x => !coins.Any(c => c.Id == x.CoinId)).ToList();
        if (removeItems.Any()) context.CoinToVendingMachines.RemoveRange(removeItems);

        foreach (var coin in coins)
        {
            var oldCoins = vendingMachine.CoinToVendingMachines.FirstOrDefault(x => x.CoinId == coin.Id);
            if (oldCoins is not null)
            {
                oldCoins.Amount = coin.Amount;
                oldCoins.Disabled = coin.Disabled;
            }
            else
            {
                await context.CoinToVendingMachines.AddAsync(new CoinToVendingMachine
                {
                    VendingMachineId = vendingMachineId,
                    CoinId = coin.Id,
                    Amount = coin.Amount,
                    Disabled = coin.Disabled
                }); 
            }
        }
        await context.SaveChangesAsync();
    }
}