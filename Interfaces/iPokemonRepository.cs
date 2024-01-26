using firstapp.Models;

namespace firstapp.interfaces;

public interface iPokemonRepository{
    ICollection<Pokemon> GetPokemons();
    Pokemon GetPokemon(int id);

    Pokemon GetPokemon(string name);

    bool PokemonExist(int pokeId);

    bool CreatePokemon(int ownerId , int categoryId ,Pokemon pokemon);
}