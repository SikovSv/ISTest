namespace ISTest.Models;

public class Coin
{
    [Key]
    public int Value { get; set; }

    public bool Disabled { get; set; }

    [StringLength(100)]
    public string ImmagePath { get; set; }
}