using firstapp.interfaces;
using firstapp.Models;
using Microsoft.AspNetCore.Mvc;
using firstapp.Repository;
using System.Text.Json.Nodes;


namespace firstapp.Controllers;

 [ApiController]
public class ReviewController : Controller
{
    private readonly iReviewRepository _reviewRepository;
    private readonly iPokemonRepository _pokemonRepository;
private readonly iReviewerRepository _reviewerRepository;
    public ReviewController(iReviewRepository reviewRepository ,
    iPokemonRepository pokemonRepository ,
    iReviewerRepository reviewerRepository
    )
    {
        _reviewRepository= reviewRepository;
        _reviewerRepository = reviewerRepository;
        _pokemonRepository = pokemonRepository;
    }

    [Route("api/GetReviews")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(IEnumerable<Review>))]
    public IActionResult GetReviews(){
        var  reviews= _reviewRepository.GetReviews();
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(reviews);
    }

    [Route("api/GetReview/{id}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(Review))]
    public IActionResult GetReview(int id){
        if(! _reviewRepository.ReviewExists(id)){
            return NotFound();
        }
        var review = _reviewRepository.GetReview(id);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(review);
    }
    [Route("api/GetReviewsOfAPokemon/{pokeId}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(IEnumerable<Review>))]
    public IActionResult GetReviewsOfAPokemon(int pokeId){
        var reviews = _reviewRepository.GetReviewsOfAPokemon(pokeId);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(reviews);
    }

    [Route("api/createReview")]
    [HttpPost]
    public IActionResult createReview([FromBody] JsonNode data){
        int pokemonId = (int)data["pokemonId"];
        int reviewerId = (int)data["reviewerId"];
        string title = data["title"].ToString();
        string text = data["text"].ToString();

        if (pokemonId == null || reviewerId == null || title == null || text == null){
            return BadRequest();
        }

        Pokemon pokemon = _pokemonRepository.GetPokemon(pokemonId);
        Reviewer reviewer = _reviewerRepository.GetReviewer(reviewerId);

        Review review = new Review(){
            Text = text ,
            Title = title ,
            Pokemon = pokemon,
            Reviewer = reviewer
        };

        if ( !_reviewRepository.createReview(review)){
            ModelState.AddModelError("" , "Can not create review");
            return StatusCode(500 , ModelState);
        }

        return Ok(review);
    }
}