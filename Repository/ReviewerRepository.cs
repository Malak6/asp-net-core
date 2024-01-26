
using firstapp.Data;
using firstapp.interfaces;
using firstapp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Repository;

public class ReviewerRepository : iReviewerRepository
{
    private readonly DataContext _context;

    public ReviewerRepository(DataContext context)
    {
        _context = context;
    }

    public bool createReviewer(Reviewer reviewer)
    {
        _context.Reviewers.Add(reviewer);
        int num = _context.SaveChanges();
        return num > 0 ? true : false;
    }

    public Reviewer GetReviewer(int reviewerId)
    {
        return _context.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();

    }

    public ICollection<Reviewer> GetReviewers()
    {
        return _context.Reviewers.ToList();
        
    }

    public ICollection<Review> GetReviewsByReviewer(int reviewerId)
    {
        return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
    }

    public bool ReviewerExists(int reviewerId)
    {
        return _context.Reviewers.Any(r => r.Id == reviewerId);
    }
}