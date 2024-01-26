
using firstapp.Data;
using firstapp.interfaces;
using firstapp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Repository;

public class OwnerRepository : iOwnerRepository
{
    private readonly DataContext _context;

    public OwnerRepository(DataContext context)
    {
        _context = context;
    }

    public bool CreateOwner(Owner owner)
    {
        _context.Owners.Add(owner);
        int num = _context.SaveChanges();
        return num > 0 ? true : false;
    }

    public Owner GetOwner(int ownerId)
    {
        return _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
    }

    public ICollection<Owner> GetOwnerOfAPokemon(int pokeId)
    {
        return _context.PokemonOwners.Where(p => p.PokemonId == pokeId).Select(p => p.Owner).ToList();
    }

    public ICollection<Owner> GetOwners()
    {
        return _context.Owners.ToList();
    }

    public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
    {
         return _context.PokemonOwners.Where(o => o.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
    }

    public bool OwnerExists(int ownerId)
    {
        return _context.Owners.Any(o => o.Id == ownerId);
    }
}