using AutoMapper;
using ParcelService.DTOs;
using ParcelService.Entities;
using ParcelService.Helpers;
using ParcelService.Repositories;

namespace ParcelService.Services;

public class ParcelServ: IParcelServ
{
    private readonly IParcelRepository _parcelRepo;
    private readonly IMapper _mapper;

    public ParcelServ(IParcelRepository parcelRepository, IMapper mapper)
    {
        _parcelRepo = parcelRepository;
        _mapper = mapper;
    }

    public string GenerateTrackingNumber()
    {
        Guid guid = Guid.NewGuid();
        string shortTrackingNumber = guid.ToString().Substring(0, 8);

        return shortTrackingNumber;
    }

    public async Task<List<ParcelDto>?> GetNewParcelsAsync()
    {
        var parcels = await _parcelRepo.GetNewParcelsAsync();

        if (parcels == null || !parcels.Any()) return null;

        var parcelsDto = _mapper.Map<List<ParcelDto>>(parcels);

        return parcelsDto;
    }

    public async Task<List<ParcelDto>?> GetNewParcelsInCountryAsync(string country)
    {
        var parcels = await _parcelRepo.GetNewParcelsInCountryAsync(country);

        if (parcels == null || !parcels.Any()) return null;

        var parcelsDto = _mapper.Map<List<ParcelDto>>(parcels);

        return parcelsDto;
    }

    public async Task<List<ParcelDto>?> GetNewParcelsFromCityAsync(string city)
    {
        var parcels = await _parcelRepo.GetNewParcelsFromCityAsync(city);

        if (parcels == null || !parcels.Any()) return null;

        var parcelsDto = _mapper.Map<List<ParcelDto>>(parcels);

        return parcelsDto;
    }

    public async Task<ParcelDto> CreateParcelAsync(CreateParcelDto createParcelDto)
    {
        var parcel = _mapper.Map<Parcel>(createParcelDto);
        parcel.TrackingNumber = GenerateTrackingNumber();

        await _parcelRepo.AddParcelAsync(parcel);

        var result = await _parcelRepo.SaveChangesAsync();

        if (!result)
        {
            throw new AppException("Failed to save the parcel to the database.");
        }

        return _mapper.Map<ParcelDto>(parcel);
    }

    public async Task<bool> UpdateParcelStatusAsync(Status status, string trackingNumber)
    {
        var parcel = await _parcelRepo.GetParcelByTrackingNumberAsync(trackingNumber);

        if (parcel == null) return false;

        parcel.Status = status;

        var result = await _parcelRepo.SaveChangesAsync();

        if (!result)
        {
            throw new AppException("Failed to update the parcel status in the database.");
        }

        return result;
    }
}
