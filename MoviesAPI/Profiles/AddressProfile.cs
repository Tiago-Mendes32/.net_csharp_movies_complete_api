using AutoMapper;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace AddressAPI.Profiles;

public class AddressProfile: Profile
{
    public AddressProfile()
    {
        CreateMap<CreateAddressDto, Address>();
        CreateMap<UpdateAddressDto, Address>();
        CreateMap<Address, ReadAddressDto>();
    }
}