
using firstapp.Data;
using firstapp.interfaces;
using firstapp.Models;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Repository;

public class PokemonRepository:iPokemonRepository
{

    private readonly DataContext _context;

    public PokemonRepository(DataContext context){
        _context = context;
    }
    public ICollection<Pokemon> GetPokemons(){
        return _context.Pokemons.OrderBy(p => p.Id).ToList();
    }

    public Pokemon GetPokemon(int id)
    {
        return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
    }

    public Pokemon GetPokemon(string name)
    {
        return _context.Pokemons.Where(p =>p.Name == name).FirstOrDefault();
    }

    public bool PokemonExist(int pokeId)
    {
        return _context.Pokemons.Any(p => p.Id == pokeId);
    }

    public bool CreatePokemon(int ownerId , int categoryId ,Pokemon pokemon)
    {
        Owner owner = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        Category category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

        PokemonCategory pokemonCategory = new PokemonCategory(){
            Pokemon = pokemon ,
            Category = category
        };

        _context.PokemonCategory.Add(pokemonCategory);

        PokemonOwner pokemonOwner = new PokemonOwner(){
            Pokemon = pokemon ,
            Owner = owner
        };

        _context.PokemonOwners.Add(pokemonOwner);

        _context.Pokemons.Add(pokemon);
        var num = _context.SaveChanges();
        return num >0 ? true : false ;
    }
}