using HotelListing.API.Data;
using HotelListing.API.Dtos.Country;
using HotelListing.API.Dtos.Hotel;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Dtos
{
    public class GetCountryDto : BaseCountryDto
    {
        public int Id { get; set; }


    }

    

   
}
