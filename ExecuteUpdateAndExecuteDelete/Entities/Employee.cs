namespace ExecuteUpdateAndExcecuteDelete.Entities;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Employee
{
    public int Id { get; init; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Precision(14, 2)]
    public decimal Salary { get; set; }

    public int CompanyId { get; set; }

    public Company Company { get; set; }
}
