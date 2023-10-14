using AutoMapper;
using AutoMapperReflectionRegistration.Models.Entity;
using AutoMapperReflectionRegistration.Models.Service;

namespace AutoMapperReflectionRegistration.Engine;

public class Worker
{
    private readonly IReadOnlyCollection<Article> articles = new List<Article>
    {
        new ()
        {
            Id = 1,
            Title = "Introduction to C# Programming",
            Description = "A beginner's guide to C# programming language.",
            CreatedOn = DateTime.Parse("2023-10-14T14:52"),
            Author = new() { Id = Guid.NewGuid(), Username = "JohnDoe" }
        },
        new()
        {
            Id = 2,
            Title = "Advanced C# Concepts",
            Description = "Exploring advanced features of C#.",
            CreatedOn = DateTime.Parse("2023-09-7T11:18"),
            Author = new() { Id = Guid.NewGuid(), Username = "JaneSmith" }
        },
        new()
        {
            Id = 3,
            Title = "C# and Object-Oriented Programming",
            Description = "Understanding OOP in C#.",
            CreatedOn = DateTime.Parse("2023-10-01T12:33"),
            Author = new() { Id = Guid.NewGuid(), Username = "RobertJohnson" }
        },
        new()
        {
            Id = 4,
            Title = "C# Web Development with ASP.NET Core",
            Description = "Building web applications using ASP.NET Core and C#.",
            CreatedOn = DateTime.Parse("2023-10-13T11:23"),
            Author = new() { Id = Guid.NewGuid(), Username = "EmilyBrown" }
        },
        new()
        {
            Id = 5,
            Title = "C# Best Practices",
            Description = "Tips and best practices for writing clean and efficient C# code.",
            CreatedOn = DateTime.Parse("2023-08-08T17:02"),
            Author = new() { Id = Guid.NewGuid(), Username = "MichaelWilson" }
        }
    };

    private readonly IMapper mapper;

    public Worker(IMapper mapper)
        => this.mapper = mapper;

    public void Run()
    {
        Console.WriteLine("Articles List:");

        var articlesList = this.mapper
            .Map<IEnumerable<ArticleListingServiceModel>>(this.articles)
            .ToList();

        foreach (var article in articlesList)
        {
            Console.WriteLine(
                $"Article: {article.Id} - {article.Title} - {article.Author}");
        }

        var articlesDetails = this.mapper
            .Map<IEnumerable<ArticleDetailsServiceModel>>(this.articles)
            .ToList();

        Console.WriteLine("Articles Details:");

        foreach (var articleDetails in articlesDetails)
        {
            Console.WriteLine(
                $"Article: {articleDetails.Id} - {articleDetails.Title} - {articleDetails.Description} - {articleDetails.CreatedOn} - {articleDetails.Author}");
        }
    }
}
