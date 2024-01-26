using firstapp.interfaces;
using firstapp.Models;
using Microsoft.AspNetCore.Mvc;
using firstapp.Repository;
using System.Text.Json.Nodes;

namespace firstapp.Controllers;

 [ApiController]
public class CountryController : Controller
{
    private readonly iCountryRepository _countryRepository;

    public CountryController(iCountryRepository countryRepository)
    {
        _countryRepository= countryRepository;
    }

    [Route("api/getCountries")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(IEnumerable<Country>))]
    public IActionResult getCountries(){
        var  countries= _countryRepository.GetCountries();
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(countries);
    }

    [Route("api/getCountry/{id}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(Country))]
    public IActionResult getCountry(int id){
        if(! _countryRepository.ValidCountry(id)){
            return NotFound();
        }
        var category = _countryRepository.GetCountry(id);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(category);
    }
    [Route("api/GetCountryByOwner/{ownerId}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(Country))]
    public IActionResult GetCountryByOwner(int ownerId){
        var country = _countryRepository.GetCountryByOwner(ownerId);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(country);
    }

    [Route("api/GetOwnersByCountry/{countryId}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(IEnumerable<Owner>))]
    public IActionResult GetOwnersByCountry(int countryId){
        var owners = _countryRepository.GetOwnersByCountry(countryId);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(owners);
    }
    [Route("api/createCountry")]
    [HttpPost]
    public IActionResult createCountry([FromBody] JsonNode data){
        string name = data["name"].ToString();
        if (name == null){
            return BadRequest(ModelState);
        }
        var existCountry = _countryRepository.GetCountries().Where(c => c.Name == name).FirstOrDefault();
        if(existCountry != null){
            ModelState.AddModelError("", "Country already exist");
            return StatusCode(422 , ModelState);
        }
        Country country = new Country(){
            Name = name ,
            };

        if (! ModelState.IsValid){
            return BadRequest(ModelState);
        }
        if(!_countryRepository.CreateCountry(country)){
            ModelState.AddModelError("", "Can not add country");
            return StatusCode(500 , ModelState);
        }

        return Ok(country);
    }


}