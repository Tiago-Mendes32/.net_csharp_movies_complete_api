using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;
using System.Diagnostics;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
    private MovieContext _context;
    private IMapper _mapper;


    public AddressController(MovieContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddAddress([FromBody]CreateAddressDto addressDto)
    {
        Address address = _mapper.Map<Address>(addressDto);
        _context.Add(address);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetAddressById), new { id = address.Id }, address);
    }

    [HttpGet]
    public IEnumerable<ReadAddressDto> GetAddresss([FromQuery] int skip = 0, [FromQuery] int take = 10) 
    {
        IEnumerable<Address> addresss = _context.Addresses;
        return _mapper.Map<IEnumerable<ReadAddressDto>>(addresss).Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetAddressById(int id) 
    {
        Address address = _context.Addresses.FirstOrDefault(c => c.Id == id);
        return address == null ? NotFound() : Ok(_mapper.Map<ReadAddressDto>(address));
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAddress(int id, UpdateAddressDto addressDto) 
    {
        Address address = _context.Addresses.FirstOrDefault<Address>(address => address.Id == id);
        if (address == null) {return NotFound();}

        _mapper.Map<Address>(addressDto);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateAddressPatch(int id, [FromBody]JsonPatchDocument<UpdateAddressDto> patch) 
    {
        Address address = _context.Addresses.FirstOrDefault<Address>(address => address.Id == id);
        if (address == null) { return NotFound(); }

        UpdateAddressDto toUpdateAddress = _mapper.Map<UpdateAddressDto>(address);
        patch.ApplyTo(toUpdateAddress, ModelState);

        if (!TryValidateModel(toUpdateAddress))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(toUpdateAddress, address);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAddress(int id) {
        Address address = _context.Addresses.FirstOrDefault<Address>(address => address.Id == id);
        if (address == null) { return NotFound(); }

        _context.Remove(address);
        _context.SaveChanges();
        return NoContent();
    }
}
