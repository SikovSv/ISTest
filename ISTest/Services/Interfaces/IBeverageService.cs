
namespace ISTest.Services
{
    public interface IBeverageService
    {
        Task<Beverage> CreateBeverage(Beverage beverage);
        Task DeleteBeverage(int beverageId);
        Task<IEnumerable<Beverage>> GetAllBeverages();
        Task<IEnumerable<BeverageForVendingMachineDto>> GetBeveragesInVendingMachine(int vendingMachineId);
        Task UpdateBeverage(Beverage beverage);
    }
}