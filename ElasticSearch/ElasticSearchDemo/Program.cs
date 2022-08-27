namespace ElasticSearchDemo;

using Nest;

public class Program
{
    private static Uri node;
    private static ConnectionSettings settins;
    private static ElasticClient client;

    public static void Main()
    {
        node = new Uri("https://localhost:9200");
        settins = new ConnectionSettings(node).DefaultIndex("my_blog");
        client = new ElasticClient(settins);

        var indexSettings = new IndexSettings
        {
            NumberOfReplicas = 1,
            NumberOfShards = 1
        };

        client.Indices.Create(
            "my_blog",
            index => index
                .InitializeUsing(new IndexState
                {
                    Settings = indexSettings
                })
                .Map<Post>(p => p.AutoMap()));

        //InsertData();
        PerformTermQuery();
        PerformMatchPhrase();
        PerformFilter();
    }

    private static void InsertData()
    {
        var newBlogPost = new Post
        {
            UserId = 1,
            PostDate = DateTime.Now,
            PostText = "This is a blog post."
        };

        var pastBlogPost = new Post
        {
            UserId = 2,
            PostDate = DateTime.Now.AddDays(-2),
            PostText = "This is a blog post from the past."
        };

        var futureBlogPost = new Post
        {
            UserId = 2,
            PostDate = DateTime.Now.AddDays(5),
            PostText = "This is a blog post from the future."
        };

        client.IndexDocument(newBlogPost);
        client.IndexDocument(pastBlogPost);
        client.IndexDocument(futureBlogPost);

        Console.WriteLine("Data inserted.");
    }

    private static void PerformTermQuery()
    {
        Console.WriteLine("Term query results:");

        var result = client
            .Search<Post>(s => s
                .Query(p => p.Term(q => q.PostText, "blog")));

        result
            .Documents
            .ToList()
            .ForEach(p => Console.WriteLine(p.PostText));

        Console.WriteLine(new string('-', 40));
    }

    private static void PerformMatchPhrase()
    {
        Console.WriteLine($"Match query results:");

        var result = client
            .Search<Post>(s => s
                .Query(q => q
                    .MatchPhrase(m => m.Field(p => p.PostText)
                    .Query("past blog post"))));

        result
            .Documents
            .ToList()
            .ForEach(p => Console.WriteLine(p.PostText));

        Console.WriteLine(new string('-', 40));
    }

    private static void PerformFilter()
    {
        Console.WriteLine($"Filter query results:");

        var result = client.Search<Post>(s => s
            .Query(q => q.Bool(b => b
                .Must(m => m
                    .Match(m => m
                        .Field(f => f.PostText).Query("blog")))
                .Filter(f => f
                    .DateRange(r => r
                        .Field(f => f.PostDate).GreaterThan(DateTime.Now))))));

        result
            .Documents
            .ToList()
            .ForEach(p => Console.WriteLine(p.PostText));

        Console.WriteLine(new string('-', 40));
    }
}