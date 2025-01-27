using AutoMapper;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace CinemasAPI.Profiles;

public class CinemaProfile: Profile
{
    public CinemaProfile()
    {
        CreateMap<CreateCinemaDto, Cinema>();
        //CreateMap<UpdateCinemaDto, Cinema>();
        //CreateMap<Cinema, UpdateCinemaDto>();
        CreateMap<Cinema, ReadCinemaDto>()
            .ForMember(cinemaDto => cinemaDto.AddressDto, 
            opt => opt.MapFrom(cinema => cinema.Address));
        CreateMap<UpdateCinemaDto, Cinema>(); 
    }
}