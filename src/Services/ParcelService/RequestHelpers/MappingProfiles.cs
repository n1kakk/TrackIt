using AutoMapper;
using ParcelService.DTOs;
using ParcelService.Entities;

namespace ParcelService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Parcel, ParcelDto>();
        CreateMap<ParcelDto, Parcel>();
        CreateMap<CreateParcelDto, Parcel>();
        CreateMap<CreateParcelDto, ParcelDto>();
    }
}
