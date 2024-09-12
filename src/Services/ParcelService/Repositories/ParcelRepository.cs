using Microsoft.EntityFrameworkCore;
using ParcelService.Data;
using ParcelService.Entities;

namespace ParcelService.Repositories;

public class ParcelRepository : IParcelRepository
{
    private readonly ParcelDbContext _dbContext;
    public ParcelRepository(ParcelDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void AddParcel(Parcel parcel)
    {
        _dbContext.Parcels.Add(parcel);
    }

    public async Task<List<Parcel>?> GetNewParcels()
    {
        var parcels = await _dbContext.Parcels
            .Where(p => p.Status == Status.New).ToListAsync();

        return parcels.Any() ? parcels : null;
    }

    public async Task<List<Parcel>?> GetNewParcelsFromCity(string city)
    {
        var parcels =  await _dbContext.Parcels
            .Where(p => p.Status == Status.New && p.FromCity == city).ToListAsync();

        return parcels.Any() ? parcels : null;
    }

    public async Task<List<Parcel>?> GetNewParcelsInCountry(string country)
    {
        var parcels = await _dbContext.Parcels
            .Where(p => p.Status == Status.New && p.Country == country).ToListAsync();

        return parcels.Any() ? parcels : null;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0; 
    }

    public async Task<bool> UpdateStatus(Status status, string trackingNumber)   
    {
        var parcel = await _dbContext.Parcels.FindAsync(trackingNumber);

        if (parcel == null) return false;

        parcel.Status = status;

        await _dbContext.SaveChangesAsync();

        return true;
    }
}
