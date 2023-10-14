using AutoMapper;

namespace AutoMapperReflectionRegistration.Infrastructure.Mapping.Interfaces;

public interface IMapExplicitly
{
    void RegisterMappings(IProfileExpression profile);
}
