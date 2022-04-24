using DBMSApi.Controllers.Viewmodels;
using DBMSApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBMSApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/ingredient")]
    public class IngredientController : ControllerBase
    {
        DBMSContext _db;
        public IngredientController(DBMSContext db)
        {
            _db = db;
        }

        // GET: IngredientController
        [HttpGet]
        public List<Ingredient> Index()
        {
            return _db.ingredients.ToList();
        }

        // GET: IngredientController/Details/5
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var ingredient = _db.ingredients.Find(id);
            return ingredient == null ? NotFound() : Ok(ingredient);
        }

        [HttpPost]
        public Ingredient Create([FromBody] CreateIngredientViewModel ingredientModel)
        {
            var ingredient = new Ingredient() { 
                ingredientName = ingredientModel.ingredientName
            };

            _db.ingredients.Add(ingredient);
            _db.SaveChanges();

            return ingredient;
        }

        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            var ingredient = _db.ingredients.Find(id);
            if (ingredient == null)
                return NotFound();

            var roomateIngredients = _db.roomateIngredients.Where(x => x.ingredientId == id);
            var recipeIngredients = _db.recipeIngredients.Where(x => x.ingredientId == id);

            

            _db.ingredients.Remove(ingredient);
            _db.roomateIngredients.RemoveRange(roomateIngredients);

            // Delete all recipes using no longer existing ingredient
            foreach (var recipeIngredient in recipeIngredients)
            {
                _db.recipes.Remove(recipeIngredient.recipe);
            }

            _db.recipeIngredients.RemoveRange(recipeIngredients);

            _db.SaveChanges();

            return Ok();
        }
    }
}
