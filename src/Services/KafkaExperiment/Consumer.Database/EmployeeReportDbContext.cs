using Consumer.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Database;

public class EmployeeReportDbContext : DbContext
{
    public EmployeeReportDbContext(DbContextOptions<EmployeeReportDbContext> dbContextOptions)
        : base(dbContextOptions)
    {

    }

    public DbSet<EmployeeReport> Reports { get; set; }
}
