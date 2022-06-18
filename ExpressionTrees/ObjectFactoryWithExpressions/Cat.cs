namespace ObjectFactoryWithExpressions;

public class Cat
{
    public Cat()
    {
    }

    public Cat(string name)
        => this.Name = name;

    public string Name { get; set; }
}
