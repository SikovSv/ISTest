
namespace ISTest.Pages
{
    public partial class Index
    {
        [Inject]
        protected ICoinService CoinService { get; set; }

        [Inject]
        protected IBeverageService BeverageService { get; set; }

        protected IEnumerable<Coin> Coins { get; set; }
        protected IEnumerable<BeverageForVendingMachineDto> Beverages { get; set; }
        protected string DisplayText { get; set; }
        protected decimal Bank { get; set; }
        protected override async Task OnInitializedAsync()
        {
            SetCashText(Bank);
            Coins = await CoinService.GetAllCoins();
            Beverages = await BeverageService.GetBeveragesInVendingMachine(1);
        }

        protected void AddCoin(decimal value)
        {
            Bank += value;
            SetCashText(Bank);
        }

        protected async Task Buy(BeverageForVendingMachineDto beverage)
        {
            if (beverage.Price > Bank || beverage.Amount < 1)
            {
                SetErrorText();
            }
            else
            {
                Bank -= beverage.Price;
                SetBuyText(beverage);                
            }
            await Task.Delay(3000);
            SetCashText(Bank);
        }

        protected void SetCashText(decimal value)
        {
            if (value == 0)
                DisplayText = "Добро пожаловать!" + Environment.NewLine + "Для начала работы, внесите необходимую сумму...";
            else
                DisplayText = $"Внесена сумма: {value:N2}" + Environment.NewLine + "Выберете напиток или заберите сдачу...";
        }

        protected void SetBuyText(BeverageForVendingMachineDto beverage)
        {
            DisplayText = $"Вы купили напиток: {beverage.Name} ({beverage.Volume} л.)" + Environment.NewLine + "Благодарим за покупку!";
        }

        protected void SetErrorText()
        {
            DisplayText = "Сбой в работе!" + Environment.NewLine + "Через некоторое время автомат вернется к работе...";
        }
    }
}
