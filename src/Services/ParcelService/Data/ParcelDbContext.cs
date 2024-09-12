using Microsoft.EntityFrameworkCore;
using ParcelService.Entities;

namespace ParcelService.Data;

public class ParcelDbContext : DbContext
{
    public ParcelDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Parcel> Parcels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
