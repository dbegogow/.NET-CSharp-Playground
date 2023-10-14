﻿using AutoMapperReflectionRegistration.Infrastructure.Mapping.Interfaces;
using AutoMapper;

namespace AutoMapperReflectionRegistration.Infrastructure.Mapping;

public class ConventionalMappingProfile : Profile
{
    public ConventionalMappingProfile()
    {
        var mapFromType = typeof(IMapFrom<>);
        var mapToType = typeof(IMapTo<>);
        var explicitMapType = typeof(IMapExplicitly);

        var modelRegistrations = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .Where(a => a.FullName.StartsWith("AutoMapperReflectionRegistration"))
            .SelectMany(a => a.GetExportedTypes())
            .Where(t => t.IsClass && !t.IsAbstract)
            .Select(t => new
            {
                Type = t,
                MapFrom = this.GetMappingModel(t, mapFromType),
                MapTo = this.GetMappingModel(t, mapToType),
                ExplicitMap = t.GetInterfaces()
                    .Where(i => i == explicitMapType)
                    .Select(i => (IMapExplicitly)Activator.CreateInstance(t))
                    .FirstOrDefault()
            });

        foreach (var modelRegistration in modelRegistrations)
        {
            if (modelRegistration.MapFrom != null)
            {
                this.CreateMap(modelRegistration.MapFrom, modelRegistration.Type);
            }

            if (modelRegistration.MapTo != null)
            {
                this.CreateMap(modelRegistration.Type, modelRegistration.MapTo);
            }

            modelRegistration.ExplicitMap?.RegisterMappings(this);
        }
    }

    private Type GetMappingModel(Type type, Type mappingInterface)
        => type.GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType
                && i.GetGenericTypeDefinition() == mappingInterface)
            ?.GetGenericArguments()
            .First();
}
