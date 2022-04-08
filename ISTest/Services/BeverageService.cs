using ISTest.Data;

namespace ISTest.Services;

public class BeverageService
{
    private readonly BeverageContext _context;

    public BeverageService(BeverageContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Beverage>> GetAllBeverages()
    {
        return await _context.Beverages.ToListAsync();
    }

    public async Task<Beverage> CreateBeverage(Beverage beverage)
    {
        var entityEntry = await _context.Beverages.AddAsync(beverage);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task DeleteBeverage(int beverageId)
    {
        var entity = await _context.Beverages.FindAsync(beverageId);
        if (entity is null) return;
        _context.Beverages.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBeverage(Beverage beverage)
    {
        var entity = await _context.Beverages.FindAsync(beverage.Id);
        if (entity is null) return;
        entity.Number = beverage.Number;
        entity.Price = beverage.Price;
        await _context.SaveChangesAsync();
    }
}

builder.Services.AddDbContext<BeverageContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));