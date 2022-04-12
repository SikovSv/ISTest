﻿
namespace ISTest.Pages
{
    public partial class Index
    {
        [Inject]
        protected ICoinService CoinService { get; set; }

        [Inject]
        protected IBeverageService BeverageService { get; set; }

        protected IEnumerable<CoinDto> Coins { get; set; }
        protected IEnumerable<CoinToVendingMachineDto> VendingMachineCoins { get; set; }
        protected IEnumerable<BeverageForVendingMachineDto> Beverages { get; set; }
        protected Dictionary<CoinDto, int> ChangeBank { get; set; } = new();
        protected string DisplayText { get; set; }
        protected decimal Bank { get; set; }
        protected override async Task OnInitializedAsync()
        {
            SetCashText(Bank);
            Coins = await CoinService.GetAllCoins();
            VendingMachineCoins = await CoinService.GetAllCoins(1);
            Beverages = await BeverageService.GetBeveragesInVendingMachine(1);
        }

        protected async Task AddCoin(CoinDto coin)
        {            
            var coinVM = VendingMachineCoins.FirstOrDefault(x => x.Id == coin.Id);
            if (coinVM is null || coinVM.Disabled)
            {
                if (ChangeBank.ContainsKey(coin)) ChangeBank[coin]++;
                else ChangeBank[coin] = 1;
                SetNotAcceptText();
                await Task.Delay(3000);
                SetCashText(Bank);
            }
            else
            {
                ChangeBank.Clear();
                await CoinService.AddCoinToVendingMachine(1, coin.Id);
                Bank += coin.Value;
                coinVM.Amount++;
                SetCashText(Bank);
            }
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
                beverage.Amount--;
                await BeverageService.BuyBeverage(1, beverage.Id);
                SetBuyText(beverage);
            }
            await Task.Delay(3000);
            SetCashText(Bank);
        }

        protected async Task GetChange()
        {
            if (Bank == 0) return;
            var change = await CoinService.GetChangeFromVendingMachine(1, Bank);

            decimal returnSum = 0;
            foreach (var coinVM in change)
            {
                var coin = Coins.First(x => x.Id == coinVM.coinId);
                if (ChangeBank.ContainsKey(coin)) ChangeBank[coin] += coinVM.amount;
                else ChangeBank[coin] = coinVM.amount;
                returnSum += coin.Value * coinVM.amount;
            }
            Bank -= returnSum;

            if (Bank != 0)
            {
                SetNoMoneyText();
                await Task.Delay(3000);
            }            
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

        protected void SetNotAcceptText()
        {
            DisplayText = "Аппарат не принимает данные монеты!" + Environment.NewLine + "Попробуйте вставить другую монету";
        }

        protected void SetNoMoneyText()
        {
            DisplayText = "В аппарате закончилась сдача!" + Environment.NewLine + "Обратитесь в службу техничесой поддержки...";
        }
    }
}
