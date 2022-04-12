namespace ISTest.Dto;

public class BeverageDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string Name { get; set; }

    [Precision(4, 2)]
    [Range(0, 9999, ErrorMessage = "Стоимость должна находиться в диапазоне от 0 до 9999")]
    public decimal Price { get; set; }

    [Precision(2, 2)]
    [Range(0, 10, ErrorMessage = "Объем должен находиться в диапазоне от 0 до 10")]
    public decimal Volume { get; set; }
}
