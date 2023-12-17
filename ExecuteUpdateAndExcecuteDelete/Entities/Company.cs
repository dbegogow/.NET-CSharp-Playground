namespace ExecuteUpdateAndExcecuteDelete.Entities;

public class Company
{
    public int Id { get; init; }

    public decimal Salary { get; set; }

    public ICollection<Employee> Employees { get; init; } = new HashSet<Employee>();
}
