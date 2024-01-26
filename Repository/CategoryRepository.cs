
using firstapp.Data;
using firstapp.interfaces;
using firstapp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Repository;

public class CategoryRepository : iCategoryRepository
{
    private readonly DataContext _context;
    public CategoryRepository(DataContext context)
    {
        _context = context;
    }
    public bool CategoryExist(int id)
    {
        return _context.Categories.Any(c => c.Id == id);
    }

    public bool CreateCategory(Category category)
    {
        _context.Categories.Add(category);
        var num = _context.SaveChanges();
        return num > 0 ? true : false;
    }

    public bool DeleteCategory(int id)
    {
        Category category = _context.Categories.Find(id);
         _context.Categories.Remove(category);
         var num = _context.SaveChanges();
         return num > 0 ? true : false;

    }

    public ICollection<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }

    public ICollection<Category> GetCategoriesbyPokemon(int pokeId)
    {
        return _context.PokemonCategory.Where(c => c.PokemonId == pokeId ).Select(p=> p.Category).ToList();
    }

    public Category GetCategory(int id)
    {
        return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
    }

    public Category GetCategory(string name)
    {
        return _context.Categories.Where(c => c.Name == name).FirstOrDefault();
    }
    public ICollection<Pokemon> GetCategoryPokemons(int id)
    { 
        return _context.PokemonCategory.Where(c => c.CategoryId == id ).Select(p=> p.Pokemon).ToList();
    }

    public bool UpdateCategory(Category category)
    {
        _context.Update(category);
        var num = _context.SaveChanges();
        return num > 0 ? true : false;
    }
}