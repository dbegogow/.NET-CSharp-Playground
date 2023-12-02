namespace PrimaryConstructors;

public class AnotherService(IService service)
{
    public Distance Get()
        => service.GetDistance();
}
