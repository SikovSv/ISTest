namespace ISTest.Models;

public class Beverage
{
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string Name { get; set; }

    [Range(0,1000)]
    public int Number { get; set; }

    [Precision(4, 0)]
    [Range(0, 9999)]
    public decimal Price { get; set; }
}
