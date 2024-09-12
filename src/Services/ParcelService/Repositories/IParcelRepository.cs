using ParcelService.Entities;

namespace ParcelService.Repositories;

public interface IParcelRepository
{
    void AddParcel(Parcel parcel);
    Task<bool> SaveChangesAsync();
    Task<List<Parcel>?> GetNewParcels();
    Task<List<Parcel>?> GetNewParcelsInCountry(string country);
    Task<List<Parcel>?> GetNewParcelsFromCity(string city);
    Task<bool> UpdateStatus(Status status, int parcelId);
}
