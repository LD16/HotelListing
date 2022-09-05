using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Dtos;
using HotelListing.API.Dtos.Country;
using HotelListing.API.Dtos.Hotel;

namespace HotelListing.API.Configurations
{
    public class MapperConfig: Profile
    {

        public MapperConfig()
        {
            CreateMap<Country,  CreateCountryDto>() .ReverseMap();
            CreateMap<Country,  GetCountryDto>()    .ReverseMap();
            CreateMap<Country,  CountryDto>()       .ReverseMap();
            CreateMap<Country, UpdateCountryDto>() .ReverseMap();
            
            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<Hotel, CreateHotelDto>() .ReverseMap();
            
        }
    }
}
