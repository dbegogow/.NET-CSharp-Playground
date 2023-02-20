namespace ReflectionDelegatesDemo;

public class HomeController
{
    public HomeController()
        => this.Data = new Dictionary<string, object>
        {
            ["Name"] = "Test name"
        };

    [Data]
    public IDictionary<string, object> Data { get; set; }
}
