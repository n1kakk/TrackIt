using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParcelService.DTOs;
using ParcelService.Entities;
using ParcelService.Repositories;
using ParcelService.Services;


namespace ParcelService.Controllers;


[ApiController]
[Route("api/parcels")]
public class ParcelController: ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IParcelRepository _parcelRepo;
    private readonly ParcelServ _parcelService;

    public ParcelController(IParcelRepository parcelRepository, IMapper mapper, ParcelServ parcelServ)
    {
        _parcelRepo = parcelRepository;
        _mapper = mapper;
        _parcelService = parcelServ;
    }

    [HttpGet("newParcels")]
    public async Task<ActionResult<List<ParcelDto>>> GetNewParcels()
    {
        if(!ModelState.IsValid) return BadRequest();

        var parcels = await _parcelRepo.GetNewParcels();

        if (parcels == null) return NotFound();

        var parcelsDto = _mapper.Map<List<ParcelDto>>(parcels);

        return Ok(parcelsDto);
    }

    [HttpGet("country/{country}")]
    public async Task<ActionResult<List<ParcelDto>>> GetNewParcelsInCountry(string country)
    {
        if (!ModelState.IsValid) return BadRequest();

        var parcels = await _parcelRepo.GetNewParcelsInCountry(country);

        if(parcels == null) return NotFound();

        var parcelsDto = _mapper.Map<List<ParcelDto>>(parcels);

        return Ok(parcelsDto);
    }

    [HttpGet("city/{city}")]
    public async Task<ActionResult<List<ParcelDto>>> GetNewParcelsFromSenderCity(string city)
    {
        if (!ModelState.IsValid) return BadRequest();

        var parcels = await _parcelRepo.GetNewParcelsFromCity(city);

        if (parcels == null) return NotFound();

        var parcelsDto = _mapper.Map<List<ParcelDto>>(parcels);

        return Ok(parcelsDto);
    }

    //[HttpPut]
    //public async Task<IActionResult> UpdateParcelStatus(    )
    //{

    //}
    [HttpPost]
    public async Task<ActionResult<ParcelDto>> CreateParcel(CreateParcelDto createParcelDto)
    {
        if(!ModelState.IsValid) return BadRequest();

        var parcel = _mapper.Map<Parcel>(createParcelDto);
        parcel.TrackingNumber = _parcelService.GenerateTrackingNumber();

        _parcelRepo.AddParcel(parcel);

        var newParcel = _mapper.Map<ParcelDto>(parcel);

        var result = await _parcelRepo.SaveChangesAsync();

        if (!result) return BadRequest("Could not save to dataBase");

        return Ok(newParcel);
    }

}
