using Microsoft.EntityFrameworkCore;
using Warehouse.Models;

public class APIdbContext : DbContext
{
    public APIdbContext(DbContextOptions<APIdbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<EmployeeDeportament> EmployeeDeportaments { get; set; }
}
