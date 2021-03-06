namespace ISTest.Models;

public class Beverage
{
    public Beverage()
    {
        BeverageToVendingMachines = new HashSet<BeverageToVendingMachine>();
    }
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string Name { get; set; }

    [Precision(4, 2)]
    [Range(0, 9999)]
    public decimal Price { get; set; }

    [Precision(2, 2)]
    [Range(0, 10)]
    public decimal Volume { get; set; }

    public virtual ICollection<BeverageToVendingMachine> BeverageToVendingMachines { get; set; }
}
