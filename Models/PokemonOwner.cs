namespace firstapp.Models;
public class PokemonOwner
{
    public int PokemonId { get; set; }
    public int OwnerId { get; set; }

    public Owner Owner { get; set; }

    public Pokemon Pokemon { get; set; }
}