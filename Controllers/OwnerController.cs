using firstapp.interfaces;
using firstapp.Models;
using Microsoft.AspNetCore.Mvc;
using firstapp.Repository;
using System.Text.Json.Nodes;


namespace firstapp.Controllers;

 [ApiController]
public class OwnerController : Controller
{
    private readonly iOwnerRepository _ownerRepository;
    private readonly iCountryRepository _countryRepository;

    public OwnerController(iOwnerRepository ownerRepository , iCountryRepository countryRepository)
    {
        _ownerRepository= ownerRepository;
        _countryRepository = countryRepository;
    }

    [Route("api/GetOwners")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(IEnumerable<Owner>))]
    public IActionResult GetOwners(){
        var  owners= _ownerRepository.GetOwners();
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(owners);
    }

    [Route("api/GetOwner/{id}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(Owner))]
    public IActionResult GetOwner(int id){
        if(! _ownerRepository.OwnerExists(id)){
            return NotFound();
        }
        var owner = _ownerRepository.GetOwner(id);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(owner);
    }
    [Route("api/GetOwnerOfAPokemon/{pokeId}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(Owner))]
    public IActionResult GetCountryByOwner(int pokeId){
        var owner = _ownerRepository.GetOwnerOfAPokemon(pokeId);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(owner);
    }

    [Route("api/GetPokemonByOwner/{ownerId}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(IEnumerable<Pokemon>))]
    public IActionResult GetOwnersByCountry(int ownerId){
        var pokemons = _ownerRepository.GetPokemonByOwner(ownerId);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(pokemons);
    }

    [Route("api/createOwner")]
    [HttpPost]
    public IActionResult createOwner([FromBody] JsonNode data){
        
        string name = data["name"].ToString();
        string gym =  data["gym"].ToString();
        int countryId = (int)data["countryId"];

        if (name == null || gym == null || countryId == null){
            return BadRequest(ModelState);
        }
        bool ValidCountry = _countryRepository.ValidCountry(countryId);
        if( ! ValidCountry){
            return BadRequest(ModelState);
        }

        Country country = _countryRepository.GetCountry(countryId);
        
        Owner owner = new Owner(){
            Name = name ,
            Gym = gym ,
            Country = country
            };

        if (! ModelState.IsValid){
            return BadRequest(ModelState);
        }
        if(!_ownerRepository.CreateOwner(owner)){
            ModelState.AddModelError("", "Can not add owner");
            return StatusCode(500 , ModelState);
        }

        return Ok(owner);
    }


}