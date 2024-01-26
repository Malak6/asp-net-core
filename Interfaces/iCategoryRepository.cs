using firstapp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace firstapp.interfaces;

public interface iCategoryRepository{
    ICollection<Category> GetCategories();

    Category GetCategory(int id);

    Category GetCategory(string name);

    ICollection<Pokemon> GetCategoryPokemons(int id);

    ICollection<Category> GetCategoriesbyPokemon(int pokeId);

    bool CategoryExist(int id);

    bool CreateCategory(Category category);

    bool UpdateCategory(Category category);

    bool DeleteCategory(int id);

}