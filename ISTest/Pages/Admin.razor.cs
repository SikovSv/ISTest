using Microsoft.AspNetCore.Components.Forms;
namespace ISTest.Pages;

public partial class Admin
{
    [Parameter]
    public string AdminKey { get; set; }

    [Inject]
    protected IConfiguration Configuration { get; set; }

    [Inject]
    protected ICoinService CoinService { get; set; }

    [Inject]
    protected IBeverageService BeverageService { get; set; }

    protected IEnumerable<CoinDto> Coins { get; set; }
    protected List<CoinToVendingMachineDto> VendingMachineCoins { get; set; }
    protected IEnumerable<BeverageDto> Beverages { get; set; }
    protected List<BeverageForVendingMachineDto> VendingMachineBeverages { get; set; }

    protected EditForm EditVMForm { get; set; }
    protected EditForm EditBeveragesForm { get; set; }

    protected EditForm EditVendingMachineCoinsForm { get; set; }

protected bool IsAccess { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IsAccess = Configuration["AdminKey"] == AdminKey;

        Coins = await CoinService.GetAllCoins();
        VendingMachineCoins = (await CoinService.GetAllCoins(1)).ToList();
        Beverages = await BeverageService.GetAllBeverages();
        VendingMachineBeverages = (await BeverageService.GetBeveragesInVendingMachine(1)).ToList();

        if(EditVMForm is not null)
        EditVMForm.EditContext.OnValidationStateChanged += (sender, e) =>
        {
            var isInvalid = !EditVMForm.EditContext.Validate();
            throw new Exception();

        };
    }
    protected async Task VendingMachineBeveragesSubmit()
    {
        if (EditVMForm.EditContext.Validate())
        {

        }
    }

    protected async Task BeveragesSubmit()
    {
        if (EditBeveragesForm.EditContext.Validate())
        {

        }
    }

    protected async Task VendingMachineCoinsSubmit()
    {
        if (EditBeveragesForm.EditContext.Validate())
        {

        }
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
