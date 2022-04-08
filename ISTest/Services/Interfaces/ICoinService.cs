
namespace ISTest.Services
{
    public interface ICoinService
    {
        Task<IEnumerable<Coin>> GetAllCoins();
    }
}