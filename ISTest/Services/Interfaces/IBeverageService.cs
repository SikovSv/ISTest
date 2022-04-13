namespace ISTest.Services
{
    public interface IBeverageService
    {
        Task BuyBeverage(int vendingMachineId, int beverageId);
        Task<BeverageDto> CreateBeverage(BeverageDto beverage);
        Task DeleteBeverage(int beverageId);
        Task<ICollection<BeverageDto>> GetAllBeverages();
        ICollection<BeverageDto> GetBeveragesFromStream(MemoryStream stream);
        Task<ICollection<BeverageForVendingMachineDto>> GetBeveragesInVendingMachine(int vendingMachineId);
        Task UpdateBeverage(BeverageDto beverage);
        Task UpdateBeverages(IEnumerable<BeverageDto> beverages);
        Task UpdateVendingMachineBeverage(int vendingMachineId, IEnumerable<BeverageForVendingMachineDto> beverages);
    }
}