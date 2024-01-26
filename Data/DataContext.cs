using System.Runtime.Intrinsics.X86;
using firstapp.Models;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Data;

public class DataContext : DbContext{
    public DataContext(DbContextOptions<DataContext> options) :base(options){

    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Reviewer> Reviewers { get; set; }
    public DbSet<PokemonOwner> PokemonOwners { get; set; }
    public DbSet<PokemonCategory> PokemonCategory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PokemonCategory>()
        .HasKey(pc => new {pc.PokemonId  , pc.CategoryId});   
        modelBuilder.Entity<PokemonCategory>()
        .HasOne(p => p.Pokemon)
        .WithMany(pc => pc.PokemonCategorys)
        .HasForeignKey(pc => pc.PokemonId);
        modelBuilder.Entity<PokemonCategory>()
        .HasOne(c => c.Category)
        .WithMany(pc => pc.PokemonCategorys)
        .HasForeignKey(pc => pc.CategoryId);

        modelBuilder.Entity<PokemonOwner>()
        .HasKey(po => new {po.PokemonId , po.OwnerId});
        modelBuilder.Entity<PokemonOwner>()
        .HasOne(p => p.Pokemon)
        .WithMany(po => po.PokemonOwners)
        .HasForeignKey(po => po.PokemonId);
        modelBuilder.Entity<PokemonOwner>()
        .HasOne(o => o.Owner)
        .WithMany(po => po.PokemonOwners)
        .HasForeignKey(po => po.OwnerId);


    } 


}