using AutoMapperReflectionRegistration.Models.Entity;
using AutoMapperReflectionRegistration.Infrastructure.Mapping;
using AutoMapper;

namespace AutoMapperReflectionRegistration.Models.Service;

public class ArticleListingServiceModel : IMapFrom<Article>, IMapTo<Article>, IMapExplicitly
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public void RegisterMappings(IProfileExpression profile)
    {
        profile
            .CreateMap<Article, ArticleListingServiceModel>()
            .ForMember(m => m.Author, cfg => cfg.MapFrom(a => a.Author.Username));
    }
}
