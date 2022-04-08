using AutoMapper;

namespace ISTest.Data;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Beverage, BeverageForVendingMachineDto>()
            .ForMember(x => x.Amount, x => x.MapFrom(y => y.BeverageToVendingMachines.First().Number));
    }
}
