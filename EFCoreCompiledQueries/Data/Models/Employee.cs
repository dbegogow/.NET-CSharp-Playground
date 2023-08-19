using EFCoreCompiledQueries.Data.Models.Base;

namespace EFCoreCompiledQueries.Data.Models;

public class Employee : BaseEntity
{
    public Employee()
    {
        this.Customers = new HashSet<Customer>();
        this.InverseReportsToNavigation = new HashSet<Employee>();
    }

    public string LastName { get; set; }

    public string FirstName { get; set; }

    public string Title { get; set; }

    public int ReportsTo { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime HireDate { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public string PostalCode { get; set; }

    public string Phone { get; set; }

    public string Fax { get; set; }

    public string Email { get; set; }

    public Employee ReportsToNavigation { get; set; }

    public ICollection<Customer> Customers { get; set; }

    public ICollection<Employee> InverseReportsToNavigation { get; set; }
}
