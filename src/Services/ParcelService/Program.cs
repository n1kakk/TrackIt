using Microsoft.EntityFrameworkCore;
using ParcelService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ParcelDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();




app.Run();
