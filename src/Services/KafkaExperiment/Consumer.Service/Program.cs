using Consumer.Database;
using Consumer.Service;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<EmployeeReportDbContext>(opt =>
    opt.UseSqlServer("Data Source=.;Initial Catalog=MyReportDb;Integrated Security=SSPI;MultipleActiveResultSets=True;TrustServerCertificate=True"), ServiceLifetime.Singleton);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
