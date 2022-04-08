namespace ISTest.Models;

public class VendingMachine
{
    public VendingMachine()
    {
        CoinToVendingMachines = new HashSet<CoinToVendingMachine>();
        BeverageToVendingMachines = new HashSet<BeverageToVendingMachine>();
    }
    public int Id { get; set; }

    [StringLength(100)]
    [Required]
    public string Name { get; set; }

    public virtual ICollection<CoinToVendingMachine> CoinToVendingMachines { get; set; }
    public virtual ICollection<BeverageToVendingMachine> BeverageToVendingMachines { get; set; }
}

