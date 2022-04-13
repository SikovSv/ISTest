using AutoMapper;

namespace ISTest.Data;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Beverage, BeverageDto>().ReverseMap();
        CreateMap<Beverage, BeverageForVendingMachineDto>()
            .ForMember(x => x.Amount, x => x.MapFrom(y => y.BeverageToVendingMachines.First().Number));
        CreateMap<Coin, CoinDto>();
        CreateMap<Coin, CoinToVendingMachineDto>()
            .ForMember(x => x.Amount, x => x.MapFrom(y => y.CoinToVendingMachines.Any() ? y.CoinToVendingMachines.First().Amount : 0))
            .ForMember(x => x.Disabled, x => x.MapFrom(y => y.CoinToVendingMachines.Any() ? y.CoinToVendingMachines.First().Disabled : true));
    }
}
