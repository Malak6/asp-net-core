using firstapp.Repository;
using firstapp.interfaces;

using Microsoft.AspNetCore.Mvc;
using firstapp.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.ObjectiveC;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Nodes;

namespace firstapp.Controllers;


[ApiController]
public class PokemonController : Controller{
    private readonly iPokemonRepository _pokemonRepository;
    private readonly iCategoryRepository _categoryRepository;

    private readonly iOwnerRepository _ownerRepository;


    public PokemonController(iPokemonRepository pokemonRepository ,
    iCategoryRepository categoryRepository,
    iOwnerRepository ownerRepository
    )
    {
        _pokemonRepository = pokemonRepository;
        _categoryRepository = categoryRepository;
        _ownerRepository = ownerRepository;
    }
    [Route("api/getPokemon")]
    [HttpGet]
    // [ProducesResponseType(200,Type = typeof(IEnumerable<Pokemon>))]
    public IActionResult getPokemonList(){
        var pokemons= _pokemonRepository.GetPokemons();
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(pokemons);
    }

    [Route("api/getPokemon/{id}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(Pokemon))]
    public IActionResult getPokemon(int id){

        if(!_pokemonRepository.PokemonExist(id)) return NotFound();
        var pokemon= _pokemonRepository.GetPokemon(id);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(pokemon);
    }

    [Route("api/getPokemonname/{name}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(Pokemon))]
    public  IActionResult getPokemonByName(string name){
        var pokemon = _pokemonRepository.GetPokemon(name);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(pokemon);
    }
    
    [Route("api/createPokemon")]
    [HttpPost]
    public IActionResult createPokemon([FromBody] JsonNode data){
        string name = data["name"].ToString();
        string date = data["birthDate"].ToString();
        int ownerId = (int)data["ownerId"];
        int categoryId = (int)data["categoryId"];
         if (name == null || date == null || ownerId == null || categoryId == null){
            return BadRequest(ModelState);
        }
        DateTime birthDate;
        if ( ! DateTime.TryParse(date, out birthDate)){
            return BadRequest(ModelState);
        }

        bool catExist = _categoryRepository.CategoryExist(categoryId);
        bool ownExist = _ownerRepository.OwnerExists(ownerId);

        if(! catExist || ! ownExist){
            ModelState.AddModelError("" , "Check your ids");
            return StatusCode(500 , ModelState);
        }
       
        var existPokemon = _pokemonRepository.GetPokemons().Where(c => c.Name == name).FirstOrDefault();
        if(existPokemon != null){
            ModelState.AddModelError("", "Pokemon already exist");
            return StatusCode(422 , ModelState);
        }
        Pokemon pokemon = new Pokemon(){
            Name = name ,
            BirthDate = birthDate
            };

        if (! ModelState.IsValid){
            return BadRequest(ModelState);
        }
        if(!_pokemonRepository.CreatePokemon(ownerId , categoryId,  pokemon)){
            ModelState.AddModelError("", "Can not add country");
            return StatusCode(500 , ModelState);
        }
        return Ok(pokemon);
    }

}

