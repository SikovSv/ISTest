using ISTest.Components;
using Microsoft.AspNetCore.Components.Forms;
namespace ISTest.Pages;

public partial class Admin
{
    [Parameter]
    public string AdminKey { get; set; }

    [Parameter]
    public int VendingMachineId { get; set; } = 1;

    [Inject]
    protected IConfiguration Configuration { get; set; }

    [Inject]
    protected ICoinService CoinService { get; set; }

    [Inject]
    protected IBeverageService BeverageService { get; set; }

    protected IEnumerable<CoinDto> Coins { get; set; }
    protected ICollection<CoinToVendingMachineDto> VendingMachineCoins { get; set; }
    protected ICollection<BeverageDto> Beverages { get; set; }
    protected ICollection<BeverageForVendingMachineDto> VendingMachineBeverages { get; set; }

    protected EditBeverageDialog EditBeverageDialog { get; set; }

    protected EditForm EditVendingMachineBeveragesForm { get; set; }
    protected EditForm EditBeveragesForm { get; set; }
    protected EditForm EditVendingMachineCoinsForm { get; set; }

    protected bool IsAccess { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IsAccess = Configuration["AdminKey"] == AdminKey;

        Coins = await CoinService.GetAllCoins();
        VendingMachineCoins = await CoinService.GetAllCoins(VendingMachineId);
        Beverages = await BeverageService.GetAllBeverages();
        VendingMachineBeverages = await BeverageService.GetBeveragesInVendingMachine(VendingMachineId);

        if (EditVendingMachineBeveragesForm is not null)
            EditVendingMachineBeveragesForm.EditContext.OnValidationStateChanged += (sender, e) =>
            {
                var isInvalid = !EditVendingMachineBeveragesForm.EditContext.Validate();
                throw new Exception();

            };
    }

    protected void AddBeverageToVendingMachine(BeverageDto beverage)
    {
        if (VendingMachineBeverages.Any(x => x.Id == beverage.Id)) return;

        VendingMachineBeverages.Add(new BeverageForVendingMachineDto
        {
            Id = beverage.Id,
            Name = beverage.Name,
            Volume = beverage.Volume,
            Price = beverage.Price,
            Amount = 10
        });
    }

    protected void AddCoinToVendingMachine(CoinDto coin)
    {
        if (VendingMachineCoins.Any(x => x.Id == coin.Id)) return;

        VendingMachineCoins.Add(new CoinToVendingMachineDto
        {
            Id = coin.Id,
            Value = coin.Value,
            ImagePath = coin.ImagePath,
            Disabled = false,
            Amount = 10
        });
    }

    protected async Task VendingMachineBeveragesSubmit()
    {
        if (EditVendingMachineBeveragesForm.EditContext.Validate())
        {
            await BeverageService.UpdateVendingMachineBeverage(VendingMachineId, VendingMachineBeverages);
            Beverages = await BeverageService.GetAllBeverages();
            VendingMachineBeverages = await BeverageService.GetBeveragesInVendingMachine(VendingMachineId);
        }
    }

    protected async Task BeveragesSubmit()
    {
        if (EditBeveragesForm.EditContext.Validate())
        {
            await BeverageService.UpdateBeverages(Beverages);
            Beverages = await BeverageService.GetAllBeverages();
            VendingMachineBeverages = await BeverageService.GetBeveragesInVendingMachine(VendingMachineId);
        }
    }

    protected async Task VendingMachineCoinsSubmit()
    {
        if (EditVendingMachineCoinsForm.EditContext.Validate())
        {
            await CoinService.UpdateVendingMachineCoins(VendingMachineId, VendingMachineCoins);
        }
    }

    protected void DeleteBeverage(BeverageDto beverage)
    {
        Beverages.Remove(beverage);
    }

    protected void AddBeverage()
    {
        Beverages.Add(new BeverageDto
        {
            Name = "*",
            Price = 10,
            Volume = 0.5m
        });
    }

    protected void DeleteVendingMachineBeverage(BeverageForVendingMachineDto beverage)
    {
        VendingMachineBeverages.Remove(beverage);
    }

    protected void DeleteVendingMachineCoin(CoinToVendingMachineDto coin)
    {
        VendingMachineCoins.Remove(coin);
    }
}
