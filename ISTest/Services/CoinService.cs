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

    public async Task<IEnumerable<CoinDto>> GetAllCoins()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Coins.ProjectTo<CoinDto>(_mapper.ConfigurationProvider).OrderBy(x=>x.Value).ToListAsync();
    }

    public async Task<IEnumerable<CoinToVendingMachineDto>> GetAllCoins(int vendingMachineId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Coins.ProjectTo<CoinToVendingMachineDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
}