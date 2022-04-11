using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISTest.Data;

namespace ISTest.Services;

public class BeverageService : IBeverageService
{
    private readonly IDbContextFactory<BeverageContext> _contextFactory;
    private readonly IMapper _mapper;

    public BeverageService(IDbContextFactory<BeverageContext> contextFactory, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Beverage>> GetAllBeverages()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Beverages.ToListAsync();
    }

    public async Task<Beverage> CreateBeverage(Beverage beverage)
    {
        using var context = _contextFactory.CreateDbContext();
        var entityEntry = await context.Beverages.AddAsync(beverage);
        await context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task DeleteBeverage(int beverageId)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.Beverages.FindAsync(beverageId);
        if (entity is null) return;
        context.Beverages.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateBeverage(Beverage beverage)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.Beverages.FindAsync(beverage.Id);
        if (entity is null) return;
        entity.Name = beverage.Name;
        entity.Volume = beverage.Volume;
        entity.Price = beverage.Price;
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<BeverageForVendingMachineDto>> GetBeveragesInVendingMachine(int vendingMachineId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Beverages.Where(x => x.BeverageToVendingMachines.Any(x => x.VendingMachineId == vendingMachineId))
            .ProjectTo<BeverageForVendingMachineDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
}
