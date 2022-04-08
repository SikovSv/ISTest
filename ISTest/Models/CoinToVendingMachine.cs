namespace ISTest.Models;

public class CoinToVendingMachine
{
    public int CoinId { get; set; }
    public int VendingMachineId { get; set; }
    public bool Disabled { get; set; }

    [Range(0, 1000)]
    public int Amount { get; set; }
    public virtual Coin Coin { get; set; }
    public virtual VendingMachine VendingMachine { get; set; }
}

