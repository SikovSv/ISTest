namespace ISTest.Models;

public class Coin
{
    public Coin()
    {
        CoinToVendingMachines = new HashSet<CoinToVendingMachine>();
    }

    public int Id { get; set; }

    [Precision(5, 2)]
    [Range(0, 9999)]
    public decimal Value { get; set; }

    [StringLength(100)]
    public string ImagePath { get; set; }

    public virtual ICollection<CoinToVendingMachine> CoinToVendingMachines { get; set; }
}

