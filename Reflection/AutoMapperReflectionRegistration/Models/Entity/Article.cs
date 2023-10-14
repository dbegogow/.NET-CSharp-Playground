namespace AutoMapperReflectionRegistration.Models.Entity;

public class Article
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime CreatedOn { get; set; }

    public Author Author { get; set; }
}
