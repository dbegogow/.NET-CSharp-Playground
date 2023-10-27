namespace CustomTestRunner.Framework;

[AttributeUsage(AttributeTargets.Class)]
public class SubjectAttribute : Attribute
{
    public SubjectAttribute(string name)
        => this.Name = name;

    public string Name { get; set; }
}
