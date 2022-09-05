using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Dtos;
using AutoMapper;
using HotelListing.API.Dtos.Country;
using HotelListing.API.Interfaces;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ICountriesRepository _repository;

        public CountriesController(IMapper mapper, ICountriesRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _repository.GetAllAsync();
            var map = _mapper.Map<List<GetCountryDto>>(countries);

            return Ok(map);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _repository.GetDetails(id);
            

            if (country == null)
            {
                return NotFound();
            }

            var countryDto = _mapper.Map<CountryDto>(country);

            return Ok(countryDto);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updCountry)
        {
            if (id != updCountry.Id)
            {
                return BadRequest();
            }

            var country = await _repository.GetAsync(id);

            if (country == null)
            {
                return NotFound();
            }
             
            _mapper.Map(updCountry, country);


            try
            {
                await _repository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Updated");
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto countryDto)
        {

            var country = _mapper.Map<Country>(countryDto);

            await _repository.AddAsync(country);

            return Ok("Created");
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _repository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return Ok("deleted");
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
