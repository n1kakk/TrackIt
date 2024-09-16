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
    public async Task AddParcelAsync(Parcel parcel)
    {
        await _dbContext.Parcels.AddAsync(parcel);
    }

    public async Task<List<Parcel>?> GetNewParcelsAsync()
    {
        var parcels = await _dbContext.Parcels
            .Where(p => p.Status == Status.New).ToListAsync();

        return parcels.Any() ? parcels : null;
    }

    public async Task<List<Parcel>?> GetNewParcelsFromCityAsync(string city)
    {
        var parcels = await _dbContext.Parcels
            .Where(p => p.Status == Status.New && p.FromCity == city).ToListAsync();

        return parcels.Any() ? parcels : null;
    }

    public async Task<List<Parcel>?> GetNewParcelsInCountryAsync(string country)
    {
        var parcels = await _dbContext.Parcels
            .Where(p => p.Status == Status.New && p.Country == country).ToListAsync();

        return parcels.Any() ? parcels : null;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }

    //public async Task<bool> UpdateStatusAsync(Status status, string trackingNumber)   
    //{
    //    var parcel = await _dbContext.Parcels.FindAsync(trackingNumber);

    //    if (parcel == null) return false;

    //    parcel.Status = status;

    //    await _dbContext.SaveChangesAsync();

    //    return true;
    //}

    public async Task<Parcel?> GetParcelByTrackingNumberAsync(string trackingNumber)
    {
        return await _dbContext.Parcels
            .FirstOrDefaultAsync(p => p.TrackingNumber == trackingNumber);
    }
}
