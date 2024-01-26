using firstapp.interfaces;
using firstapp.Models;
using Microsoft.AspNetCore.Mvc;
using firstapp.Repository;
using System.Text.Json.Nodes;


namespace firstapp.Controllers;

 [ApiController]
public class CategoryController : Controller
{
    private readonly iCategoryRepository _categoryRepository;

    public CategoryController(iCategoryRepository categoryRepository)
    {
        _categoryRepository= categoryRepository;
    }

    [Route("api/getCategories")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(IEnumerable<Category>))]
    public IActionResult getCategories(){
        var categories = _categoryRepository.GetCategories();
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(categories);
    }

    [Route("api/getCategory/{id}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(Category))]
    public IActionResult getCategory(int id){
        if(! _categoryRepository.CategoryExist(id)){
            return NotFound();
        }
        var category = _categoryRepository.GetCategory(id);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(category);
    }
   
    [Route("api/getCategoryByName/{name}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(Category))]
    public IActionResult getCategoryByName(string name){
        var category = _categoryRepository.GetCategory(name);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(category);
    }

    [Route("api/getCategoryPokemons/{catid}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(IEnumerable<Pokemon>))]
    public IActionResult getCategoryPokemons(int catId){
        var pokemons = _categoryRepository.GetCategoryPokemons(catId);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(pokemons);
    }

    [Route("api/createCategory")]
    [HttpPost]
    public IActionResult createCategory([FromBody] JsonNode data){
        string name = data["name"].ToString();
        if (name == null){
            return BadRequest(ModelState);
        }
        var existCategory = _categoryRepository.GetCategories().Where(c => c.Name == name).FirstOrDefault();
        if(existCategory != null){
            ModelState.AddModelError("", "Category already exist");
            return StatusCode(422 , ModelState);
        }
        Category category = new Category(){
            Name = name ,
            };

        if (! ModelState.IsValid){
            return BadRequest(ModelState);
        }
        if(!_categoryRepository.CreateCategory(category)){
            ModelState.AddModelError("", "Can not add category");
            return StatusCode(500 , ModelState);
        }

        return Ok(category);
    }

    [Route("api/GetCategoriesbyPokemon/{pokeId}")]
    [HttpGet]
    [ProducesResponseType(200,Type = typeof(IEnumerable<Category>))]
    public IActionResult GetCategoriesbyPokemon(int pokeId){
        var categories = _categoryRepository.GetCategoriesbyPokemon(pokeId);
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        return Ok(categories);
    }
    [Route("api/updateCategory/{catId}")]
    [HttpPut]
    public IActionResult UpdateCategory([FromBody] JsonNode data , int catId){
        string name = data["name"].ToString();
        // int categoryId = (int)data["categoryId"];
        // || categoryId == null
        if (name == null ){
            return BadRequest(ModelState);
        }
        // is exist 
        bool categoryExist = _categoryRepository.CategoryExist(catId);
        if (! categoryExist){
            return NotFound();
        }
        Category category = _categoryRepository.GetCategory(catId);
        category.Name = name;
        if (! _categoryRepository.UpdateCategory(category)){
            ModelState.AddModelError("" , "Something went wrong");
            return StatusCode(500  , ModelState);
        }
        return Ok(category);
    }
    [Route("api/deleteCategory/{catId}")]
    [HttpDelete]
    public IActionResult deleteCategory(int catId){
        if (!_categoryRepository.DeleteCategory(catId)){
            return BadRequest();
        }
        return Ok();
    }


}