using DBMSApi.Controllers.Viewmodels;
using DBMSApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DBMSApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        DBMSContext _db;
        public RecipeController(DBMSContext db)
        {
            _db = db;
        }

        // GET: api/<RecipeController>
        [HttpGet]
        public IEnumerable<RecipeResponseModel> Get()
        {
            var recipes = _db.recipes.ToList();

            var recipesResponse = new List<RecipeResponseModel>();

            foreach (var recipe in recipes)
            {
                _db.Entry(recipe).Collection(x => x.recipeIngredients).Load();
                _db.Entry(recipe).Collection(x => x.ingredients).Load();

                recipesResponse.Add(RecipeResponseModel.recipeResponseBuilder(recipe));
            }

            return recipesResponse;
        }

        // GET api/<RecipeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var recipe = _db.recipes.Find(id);

            if (recipe == null)
                return NotFound();

            _db.Entry(recipe).Collection(x => x.recipeIngredients).Load();
            _db.Entry(recipe).Collection(x => x.ingredients).Load();

            return Ok(RecipeResponseModel.recipeResponseBuilder(recipe));
        }

        // POST api/<RecipeController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateRecipeViewModel data)
        {
            var recipe = new Recipe()
            {
                recipeName = data.name,
                description = data.description,
            };

            _db.Add(recipe);
            _db.SaveChanges();

            return Ok(recipe);
        }

        /// <summary>
        /// Update recipe metadata
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns>Modified recipe</returns>
        [HttpPost("{id}")]
        public IActionResult updateRecipeMetaData(int id, [FromBody] UpdateRecipeMetadata data)
        {
            var recipe = _db.recipes.Find(id);

            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }

            recipe.recipeName = data.name == null ? recipe.recipeName : data.name;
            recipe.description = data.description == null ? recipe.description : data.description;

            _db.SaveChanges();
            return Ok(recipe);
        }

        /// <summary>
        /// Add ingredient to recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("addingredient/{recipeId}")]
        public IActionResult addIngredient(int recipeId, [FromBody] AddIngredientToRecipeViewModel data)
        {
            var recipe = _db.recipes.Find(recipeId);
            var ingredient = _db.ingredients.Find(data.ingredientId);

            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }

            if (ingredient == null)
            {
                return NotFound("Ingredient Not Found");
            }

            var recipeIngredient = new RecipeIngredient()
            {
                recipe = recipe,
                ingredient = ingredient,
                ingredientAmount = data.amount,
                ingredientUnit = data.unit
            };

            _db.recipeIngredients.Add(recipeIngredient);
            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete("removeIngredient/{recipeId}/{ingredientId}")]
        public IActionResult removeIngredient(int recipeId, int ingredientId)
        {
            var recipeIngredient = _db.recipeIngredients.Where(x => x.recipeId == recipeId && x.ingredientId == ingredientId).FirstOrDefault();

            if (recipeIngredient == null)
            {
                return NotFound("Ingredient Not Found");
            }

            _db.recipeIngredients.Remove(recipeIngredient);
            _db.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Delete recipe and all related recipeingredients
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var recipe = _db.recipes.Find(id);

            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }

            _db.Entry(recipe).Collection(x => x.recipeIngredients).Load();

            // Find recipe ingredients
            foreach (var ingredient in recipe.recipeIngredients)
            {
                _db.recipeIngredients.Remove(ingredient);
            }

            _db.recipes.Remove(recipe);

            _db.SaveChanges();
            return Ok();
        }
    }
}
