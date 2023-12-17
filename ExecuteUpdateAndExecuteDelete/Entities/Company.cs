namespace ExecuteUpdateAndExcecuteDelete.Entities;

using System.ComponentModel.DataAnnotations;

public class Company
{
    public int Id { get; init; }

    [StringLength(30)]
    public string Name { get; set; }

    public ICollection<Employee> Employees { get; init; } = new HashSet<Employee>();
}
