
namespace ISTest.Services
{
    public interface ICoinService
    {
        Task<IEnumerable<CoinToVendingMachineDto>> GetAllCoins(int vendingMachineId);
        Task<IEnumerable<CoinDto>> GetAllCoins();
    }
}