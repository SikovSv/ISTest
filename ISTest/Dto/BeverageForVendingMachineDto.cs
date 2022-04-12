namespace ISTest.Dto;

public class BeverageForVendingMachineDto : BeverageDto
{

    [Range(0, 1000, ErrorMessage = "Количество должно находиться в диапазоне от 0 до 1000")]
    public int Amount { get; set; }

}
