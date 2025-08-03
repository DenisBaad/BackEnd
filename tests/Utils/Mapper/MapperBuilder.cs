using AutoMapper;
using Aquiles.Application.Servicos;

namespace CommonTestUtilities.Mapper;
public class MapperBuilder
{
    public static IMapper Build()
    {
        return new MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapperConfig());
        }).CreateMapper();
    }
}
