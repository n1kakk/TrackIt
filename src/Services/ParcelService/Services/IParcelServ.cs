using ParcelService.DTOs;
using ParcelService.Entities;

namespace ParcelService.Services;

public interface IParcelServ
{
    string GenerateTrackingNumber();
    Task<List<ParcelDto>?> GetNewParcelsAsync();
    Task<List<ParcelDto>?> GetNewParcelsInCountryAsync(string country);
    Task<List<ParcelDto>?> GetNewParcelsFromCityAsync(string city);
    Task<ParcelDto> CreateParcelAsync(CreateParcelDto createParcelDto);
    Task<bool> UpdateParcelStatusAsync(Status status, string trackingNumber);
}
