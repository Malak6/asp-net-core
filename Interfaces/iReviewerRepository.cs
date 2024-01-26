using firstapp.Models;

namespace firstapp.interfaces
{
    public interface iReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int reviewerId);
        ICollection<Review> GetReviewsByReviewer(int reviewerId);
        bool ReviewerExists(int reviewerId);
        bool createReviewer(Reviewer reviewer);

    }
}