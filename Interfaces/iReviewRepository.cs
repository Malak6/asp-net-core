using firstapp.Models;

namespace firstapp.interfaces
{
    public interface iReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsOfAPokemon(int pokeId);
        bool ReviewExists(int reviewId);

        bool createReview( Review review);
    }
}