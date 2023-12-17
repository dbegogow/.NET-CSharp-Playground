namespace ExecuteUpdateAndExcecuteDelete.Entities;

public class Employee
{
    public int Id { get; init; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int CompanyId { get; set; }

    public Company Company { get; set; }
}
