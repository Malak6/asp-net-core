namespace firstapp.Models;

public class PokemonCategory
{
    public int PokemonId { get; set; }
    public int CategoryId { get; set; }

    public Category Category { get; set; }

    public Pokemon Pokemon { get; set; }
}