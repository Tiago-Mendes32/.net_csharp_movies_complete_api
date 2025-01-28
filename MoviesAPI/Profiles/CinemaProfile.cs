using AutoMapper;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace CinemasAPI.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        // DTO to Entity mappings
        CreateMap<CreateCinemaDto, Cinema>();
        CreateMap<UpdateCinemaDto, Cinema>();

        // Entity to DTO mappings
        CreateMap<Cinema, UpdateCinemaDto>();
        CreateMap<Cinema, ReadCinemaDto>()
            .ForMember(cinemaDto => cinemaDto.Address,
                opt => opt.MapFrom(cinema => cinema.Address))
                    .ForMember(cinemaDto => cinemaDto.Sessions,
                opt => opt.MapFrom(cinema => cinema.Sessions));
        
        CreateMap<UpdateCinemaDto, Cinema>();
    }
}
