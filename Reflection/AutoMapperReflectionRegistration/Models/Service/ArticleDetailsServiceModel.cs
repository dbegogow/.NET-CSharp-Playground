using AutoMapperReflectionRegistration.Models.Entity;
using AutoMapperReflectionRegistration.Infrastructure.Mapping.Interfaces;
using AutoMapper;

namespace AutoMapperReflectionRegistration.Models.Service;

public class ArticleDetailsServiceModel : IMapFrom<Article>, IMapExplicitly
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime CreatedOn { get; set; }

    public string Author { get; set; }

    public void RegisterMappings(IProfileExpression profile)
    {
        profile
            .CreateMap<Article, ArticleDetailsServiceModel>()
            .ForMember(a => a.Author, cfg => cfg.MapFrom(a => a.Author.Username));
    }
}
