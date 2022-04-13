using AutoMapper;
using AutoMapper.QueryableExtensions;
using ISTest.Data;
using OfficeOpenXml;

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

    public async Task<ICollection<BeverageDto>> GetAllBeverages()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Beverages.ProjectTo<BeverageDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<BeverageDto> CreateBeverage(BeverageDto beverage)
    {
        using var context = _contextFactory.CreateDbContext();
        var entityEntry = await context.Beverages.AddAsync(_mapper.Map<Beverage>(beverage));
        await context.SaveChangesAsync();
        return _mapper.Map<BeverageDto>(entityEntry.Entity);
    }

    public async Task DeleteBeverage(int beverageId)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.Beverages.FindAsync(beverageId);
        if (entity is null) return;
        context.Beverages.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateBeverage(BeverageDto beverage)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.Beverages.FindAsync(beverage.Id);
        if (entity is null) return;
        entity.Name = beverage.Name;
        entity.Volume = beverage.Volume;
        entity.Price = beverage.Price;
        await context.SaveChangesAsync();
    }

    public async Task UpdateBeverages(IEnumerable<BeverageDto> beverages)
    {
        using var context = _contextFactory.CreateDbContext();
        var oldBeverages = await context.Beverages.ToListAsync();

        var removeItems = oldBeverages.Where(x => !beverages.Any(b => b.Id == x.Id)).ToList();
        if (removeItems.Any()) context.Beverages.RemoveRange(removeItems);

        foreach (var beverage in beverages)
        {
            var oldBeverage = oldBeverages.FirstOrDefault(x => x.Id == beverage.Id);
            if (oldBeverage is not null)
            {
                oldBeverage.Name = beverage.Name;
                oldBeverage.Volume = beverage.Volume;
                oldBeverage.Price = beverage.Price;
            }
            else
            {
                await context.Beverages.AddAsync(_mapper.Map<Beverage>(beverage));
            }
        }
        await context.SaveChangesAsync();
    }

    public async Task<ICollection<BeverageForVendingMachineDto>> GetBeveragesInVendingMachine(int vendingMachineId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Beverages.Where(x => x.BeverageToVendingMachines.Any(x => x.VendingMachineId == vendingMachineId))
            .ProjectTo<BeverageForVendingMachineDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task BuyBeverage(int vendingMachineId, int beverageId)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.BeverageToVendingMachines.FirstOrDefaultAsync(x => x.VendingMachineId == vendingMachineId && x.BeverageId == beverageId);
        if (entity is not null)
            entity.Number--;
        await context.SaveChangesAsync();
    }
    public async Task UpdateVendingMachineBeverage(int vendingMachineId, IEnumerable<BeverageForVendingMachineDto> beverages)
    {
        using var context = _contextFactory.CreateDbContext();
        var vendingMachine = await context.VendingMachines.Include(x => x.BeverageToVendingMachines).FirstOrDefaultAsync(x => x.Id == vendingMachineId);
        if (vendingMachine is null) return;

        var removeItems = vendingMachine.BeverageToVendingMachines.Where(x => !beverages.Any(b => b.Id == x.BeverageId)).ToList();
        if (removeItems.Any()) context.BeverageToVendingMachines.RemoveRange(removeItems);

        foreach (var beverage in beverages)
        {
            var oldBeverage = vendingMachine.BeverageToVendingMachines.FirstOrDefault(x => x.BeverageId == beverage.Id);
            if (oldBeverage is not null)
                oldBeverage.Number = beverage.Amount;
            else
            {
                await context.BeverageToVendingMachines.AddAsync(new BeverageToVendingMachine
                {
                    VendingMachineId = vendingMachineId,
                    BeverageId = beverage.Id,
                    Number = beverage.Amount
                });
            }
        }
        await context.SaveChangesAsync();
    }

    public ICollection<BeverageDto> GetBeveragesFromStream(MemoryStream stream)
    {
        var newBeverages = new List<BeverageDto>();
        try
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var excelPack = new ExcelPackage(stream);
            const int startRow = 2;
            ExcelWorksheet ws = excelPack.Workbook.Worksheets[0];

            var endRow = ws.Cells[ws.Dimension.Address].End.Row;

            for (int i = startRow; i <= endRow; i++)
            {
                try
                {
                    var beverage = new BeverageDto
                    {
                        Name = ws.Cells[i, 1].Value.ToString(),
                        Price = decimal.Parse(ws.Cells[i, 2].Value.ToString()),
                        Volume = decimal.Parse(ws.Cells[i, 3].Value.ToString())
                    };
                    newBeverages.Add(beverage);
                }
                catch (Exception)
                {
                }
            }
        }
        catch (Exception)
        {
        }

        return newBeverages;
    }
}
