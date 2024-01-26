using firstapp.Models;

namespace firstapp.interfaces
{
    public interface iOwnerRepository
    {
        ICollection<Owner> GetOwners() ;
        Owner GetOwner(int ownerId);
        ICollection<Owner> GetOwnerOfAPokemon(int pokeId);
        ICollection<Pokemon> GetPokemonByOwner(int ownerId);
        bool OwnerExists(int ownerId);

        bool CreateOwner(Owner owner);

    }
}