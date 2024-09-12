using ParcelService.Entities;

namespace ParcelService.Repositories;

public class ParcelRepository : IParcelRepository
{
    public void AddParcel(Parcel parcel)
    {
        throw new NotImplementedException();
    }

    public Task<List<Parcel>> GetNewParcels()
    {
        throw new NotImplementedException();
    }

    public Task<List<Parcel>> GetNewParcelsFromCity(string city)
    {
        throw new NotImplementedException();
    }

    public Task<List<Parcel>> GetNewParcelsInCountry(string country)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UodateStatus(Status status)
    {
        throw new NotImplementedException();
    }
}
