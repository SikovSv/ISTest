﻿namespace ISTest.Dto;

public class CoinDto
{
    public int Id { get; set; }

    [Precision(5, 2)]
    [Range(0, 9999)]
    public decimal Value { get; set; }
    public string ImagePath { get; set; }

}

