namespace ISTest.Services
{
    public interface IBeverageService
    {
        Task BuyBeverage(int vendingMachineId, int beverageId);
        Task<BeverageDto> CreateBeverage(BeverageDto beverage);
        Task DeleteBeverage(int beverageId);
        Task<IEnumerable<BeverageDto>> GetAllBeverages();
        Task<IEnumerable<BeverageForVendingMachineDto>> GetBeveragesInVendingMachine(int vendingMachineId);
        Task UpdateBeverage(BeverageDto beverage);
    }
}