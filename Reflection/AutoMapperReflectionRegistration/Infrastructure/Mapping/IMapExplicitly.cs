using AutoMapper;

namespace AutoMapperReflectionRegistration.Infrastructure.Mapping;

public interface IMapExplicitly
{
    void RegisterMappings(IProfileExpression profile);
}
