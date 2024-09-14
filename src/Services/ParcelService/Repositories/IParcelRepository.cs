using ParcelService.Entities;

namespace ParcelService.Repositories;

public interface IParcelRepository
{
    Task AddParcelAsync(Parcel parcel);
    Task<bool> SaveChangesAsync();
    Task<List<Parcel>?> GetNewParcelsAsync();
    Task<List<Parcel>?> GetNewParcelsInCountryAsync(string country);
    Task<List<Parcel>?> GetNewParcelsFromCityAsync(string city);
  //  Task<bool> UpdateStatusAsync(Status status, string trackingNumber);
    Task<Parcel?> GetParcelByTrackingNumberAsync(string trackingNumber);
}
