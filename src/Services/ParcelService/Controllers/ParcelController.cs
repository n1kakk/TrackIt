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
    private readonly IParcelServ _parcelService;

    public ParcelController(IParcelRepository parcelRepository, IMapper mapper, IParcelServ parcelServ)
    {
        _parcelRepo = parcelRepository;
        _mapper = mapper;
        _parcelService = parcelServ;
    }

    [HttpGet("newParcels")]
    public async Task<ActionResult<List<ParcelDto>>> GetNewParcels()
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var parcelsDto = await _parcelService.GetNewParcelsAsync();

        if (parcelsDto == null) throw new KeyNotFoundException("No new parcels found");

        return Ok(parcelsDto);
    }

    [HttpGet("country/{country}")]
    public async Task<ActionResult<List<ParcelDto>>> GetNewParcelsInCountry(string country)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var parcelsDto = await _parcelService.GetNewParcelsInCountryAsync(country);

        if(parcelsDto == null) throw new KeyNotFoundException("No parcels found in the specified country.");

        return Ok(parcelsDto);
    }

    [HttpGet("city/{city}")]
    public async Task<ActionResult<List<ParcelDto>>> GetNewParcelsFromSenderCity(string city)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var parcelsDto = await _parcelService.GetNewParcelsFromCityAsync(city);

        if (parcelsDto == null) throw new KeyNotFoundException("No parcels found in the specified city.");

        return Ok(parcelsDto);
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateParcelStatus(Status status, string trackingNumber)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = await _parcelService.UpdateParcelStatusAsync(status, trackingNumber);

        if(result == false) throw new KeyNotFoundException("Wrong tracking number");
        return Ok("Status updated");
    }

    [HttpPost]
    public async Task<ActionResult<ParcelDto>> CreateParcel(CreateParcelDto createParcelDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var newParcel = await _parcelService.CreateParcelAsync(createParcelDto);
        return Ok(newParcel);
    }

}
