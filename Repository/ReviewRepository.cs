
using firstapp.Data;
using firstapp.interfaces;
using firstapp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Repository;

public class ReviewRepository : iReviewRepository
{
    private readonly DataContext _context;

    public ReviewRepository(DataContext context)
    {
        _context = context;
    }

    public bool createReview(Review review)
    {
        // Pokemon pokemon= _context.Pokemons.Where(p => p.Id == pokemonId).FirstOrDefault();

        // Reviewer reviewer = _context.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();

        // review.Reviewer = reviewer;
        // review.Pokemon = pokemon;
        _context.Reviews.Add(review);

        int num = _context.SaveChanges();
        return num > 0 ? true : false;
    }

    public Review GetReview(int reviewId)
    {
        return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
    }

    public ICollection<Review> GetReviews()
    {
        return _context.Reviews.ToList();
    }

    public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
    {
        return _context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList();
    }

    public bool ReviewExists(int reviewId)
    {
        return _context.Reviews.Any(r => r.Id == reviewId);
    }
}
