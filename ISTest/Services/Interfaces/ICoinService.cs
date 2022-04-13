
namespace ISTest.Services
{
    public interface ICoinService
    {
        Task AddCoinToVendingMachine(int vendingMachineId, int coinId);
        Task<ICollection<CoinToVendingMachineDto>> GetAllCoins(int vendingMachineId);
        Task<ICollection<CoinDto>> GetAllCoins();
        Task<ICollection<(int coinId, int amount)>> GetChangeFromVendingMachine(int vendingMachineId, decimal bank);
        Task UpdateVendingMachineCoins(int vendingMachineId, IEnumerable<CoinToVendingMachineDto> coins);
    }
}