using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ParcelService.Data;
using ParcelService.Helpers;
using ParcelService.Repositories;
using ParcelService.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(Program).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ParcelDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IParcelRepository, ParcelRepository>();
builder.Services.AddScoped<IParcelServ, ParcelServ>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();




app.Run();
