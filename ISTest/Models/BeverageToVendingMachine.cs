namespace ISTest.Models;

public class BeverageToVendingMachine
{
    public int BeverageId { get; set; }
    public int VendingMachineId { get; set; }

    [Range(0, 1000)]
    public int Number { get; set; }

    public virtual Beverage Beverage { get; set; }
    public virtual VendingMachine VendingMachine { get; set; }
}

