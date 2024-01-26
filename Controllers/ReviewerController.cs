using firstapp.interfaces;
using firstapp.Models;
using Microsoft.AspNetCore.Mvc;
using firstapp.Repository;
using System.Text.Json.Nodes;


namespace firstapp.Controllers;

 [ApiController]
public class ReviewerController : Controller
{
    private readonly iReviewerRepository _reviewerRepository;

    public ReviewerController(iReviewerRepository reviewerRepository)
    {
        _reviewerRepository= reviewerRepository;
    }

    [Route("api/GetReviewers")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(IEnumerable<Reviewer>))]
    public IActionResult GetReviewers(){
        var  reviewers= _reviewerRepository.GetReviewers();
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(reviewers);
    }

    [Route("api/GetReviewer/{id}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(Reviewer))]
    public IActionResult GetReviewer(int id){
        if(! _reviewerRepository.ReviewerExists(id)){
            return NotFound();
        }
        var reviewer = _reviewerRepository.GetReviewer(id);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(reviewer);
    }
    [Route("api/GetReviewsByReviewer/{reviewerId}")]
    [HttpGet]
    // [ProducesResponseType(200,Type = typeof(IEnumerable<Review>))]
    public IActionResult GetReviewsByReviewer(int reviewerId){
        var reviews = _reviewerRepository.GetReviewsByReviewer(reviewerId);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(reviews);
    }
    [Route("api/createReviewer")]
    [HttpPost]
    public IActionResult createReviewer([FromBody] JsonNode data){
        string fName = data["firstName"].ToString();
        string lName = data["lastName"].ToString();

        Reviewer reviewer = new Reviewer(){
            FirstName = fName ,
            LastName = lName
        };

        if (! _reviewerRepository.createReviewer(reviewer)){
            return BadRequest();
        }
        return Ok(reviewer);

    }
}