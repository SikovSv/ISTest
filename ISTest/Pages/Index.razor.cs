
namespace ISTest.Pages
{
    public partial class Index
    {
        [Inject]
        protected ICoinService CoinService { get; set; }

        protected IEnumerable<Coin> Coins { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Coins = await CoinService.GetAllCoins();
        }
    }
}
