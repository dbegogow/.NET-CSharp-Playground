namespace ExecuteUpdateAndExcecuteDelete.Entities;

using Microsoft.EntityFrameworkCore;

public class Company
{
    public int Id { get; init; }

    [Precision(14, 2)]
    public decimal Salary { get; set; }

    public ICollection<Employee> Employees { get; init; } = new HashSet<Employee>();
}
