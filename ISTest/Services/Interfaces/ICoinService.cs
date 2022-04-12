
namespace ISTest.Services
{
    public interface ICoinService
    {
        Task AddCoinToVendingMachine(int vendingMachineId, int coinId);
        Task<IEnumerable<CoinToVendingMachineDto>> GetAllCoins(int vendingMachineId);
        Task<IEnumerable<CoinDto>> GetAllCoins();
        Task<IEnumerable<(int coinId, int amount)>> GetChangeFromVendingMachine(int vendingMachineId, decimal bank);
    }
}