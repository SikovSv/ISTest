namespace ISTest.Dto;

public class CoinToVendingMachineDto : CoinDto
{
    public bool Disabled { get; set; }

    [Range(0, 1000, ErrorMessage = "Количество должно находиться в диапазоне от 0 до 1000")]
    public int Amount { get; set; }
}

