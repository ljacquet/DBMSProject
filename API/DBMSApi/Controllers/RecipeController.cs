using DBMSApi.Controllers.Viewmodels;
using DBMSApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DBMSApi.Controllers
{
    [Route("api/admin/[controller]")]
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
        public IEnumerable<Recipe> Get()
        {
            return _db.recipes.ToList();
        }

        // GET api/<RecipeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var recipe = _db.recipes.Find(id);

            if (recipe == null)
                return NotFound();

            return Ok(recipe);
        }

        // POST api/<RecipeController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateRecipeViewModel data)
        {
            var recipe = new Recipe()
            {
                recipeName = data.name,
                link = data.link,
                description = data.description,
            };

            // Get list of ids for comparison
            var ingredientIDList = data.ingredientIds.ConvertAll(d => d.ingredientId);

            var ingredients = _db.ingredients.Where(e => ingredientIDList.Contains(e.ingredientId));
            
            if (ingredients.Count() > data.ingredientIds.Count)
            {
                return NotFound("Unable to verify ingredient ids");
            }

            // Add to DB so we get an ID
            _db.Add(recipe);
            _db.SaveChanges();

            foreach (var ingredient in data.ingredientIds)
            {
                _db.recipeIngredients.Add(new RecipeIngredient()
                {
                    ingredientId = ingredient.ingredientId,
                    recipeId = recipe.recipeId,
                    ingredientAmount = ingredient.amount,
                    ingredientUnit = ingredient.ingredientUnit,
                });
            }

            _db.SaveChanges();
            return Ok(recipe);
        }

        /// <summary>
        /// Update recipe metadata
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns>Modified recipe</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateRecipeMetadata data)
        {
            var recipe = _db.recipes.Find(id);

            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }

            recipe.recipeName = data.name == null ? recipe.recipeName : data.name;
            recipe.description = data.description == null ? recipe.description : data.description;
            recipe.link = data.link == null ? recipe.link : data.link;

            _db.SaveChanges();
            return Ok(recipe);
        }

        /// <summary>
        /// Add ingredient to recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("/addingredient/{recipeId}")]
        public IActionResult addIngredient(int recipeId, [FromBody] AddIngredientToRecipeViewModel data)
        {
            var recipe = _db.recipes.Find(recipeId);

            if (recipe == null)
            {
                return NotFound("Recipe not found");
            }

            var recipeIngredient = new RecipeIngredient()
            {
                recipeId = recipeId,
                ingredientId = data.ingredientId,
                ingredientAmount = data.amount,
                ingredientUnit = data.unit
            };

            _db.recipeIngredients.Add(recipeIngredient);
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

            // Find recipe ingredients
            foreach (var ingredient in recipe.recipeIngredients)
            {
                _db.recipeIngredients.Remove(ingredient);
            }

            _db.SaveChanges();
            return Ok();
        }

        [HttpGet("/recipe/{id}")]
        public IActionResult getIngredients(int id)
        {
            var data = _db.recipeIngredients.Where(e => e.recipeId == id); 
            
            if (data == null)
            {
                return NotFound("Couldn't Find Ingredients");
            }

            return Ok(data);
        }
    }
}
