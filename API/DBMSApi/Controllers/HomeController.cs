using DBMSApi.Controllers.Viewmodels;
using DBMSApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DBMSApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/house")]
    public class HomeController : ControllerBase
    {
        DBMSContext dbmsContext;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(DBMSContext db, UserManager<ApplicationUser> userManager)
        {
            dbmsContext = db;
            this.userManager = userManager;

        }

        /// <summary>
        /// Lists Houses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<House>> Index()
        {
            return dbmsContext.houses.ToList();
        }

        [HttpGet("my")]
        public async Task<House> myHouse()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return null;

            var roomate = dbmsContext.roomates.Find(user.roomateId);

            var house = dbmsContext.houses.Find(roomate.houseId);

            return house;
        }
        

        /// <summary>
        /// Creates house and assigns it to user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="houseName"></param>
        /// <returns>Created House Object</returns>
        [HttpPost("create")]
        public async Task<IActionResult> createHouse([FromBody] CreateHouseModel model)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return NotFound();

            var roomate = dbmsContext.roomates.Find(user.roomateId);

            roomate.house = new House() { houseName = model.houseName, ownerId = user.roomateId };
            roomate.isOwner = true;

            dbmsContext.SaveChanges();
            return Ok(roomate.house);
        }

        [HttpPost("join/{id}")]
        public async Task<IActionResult> joinHouse(int id)
        {
            if (id == 0)
                return BadRequest();

            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return NotFound();

            var house = dbmsContext.houses.Find(id);

            if (house == null)
            {
                return NotFound("House Not Found");
            }

            var roomate = dbmsContext.roomates.Find(user.roomateId);

            if (roomate.house != null)
            {
                return BadRequest("User already in house");
            }

            roomate.house = house;
            dbmsContext.SaveChanges();

            return Ok(roomate.house);
        }

        [HttpGet("roomates")]
        public async Task<IActionResult> getRoomates()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return NotFound();

            var roomate = dbmsContext.roomates.Find(user.roomateId);
            var house = dbmsContext.houses.Find(roomate.houseId);

            if (house == null)
            {
                return NotFound("House Not Found");
            }

            var roomates = dbmsContext.roomates.Where(e => e.houseId == house.houseId).ToList();
            return Ok(roomates);
        }

        /// <summary>
        /// Deletes house and removes all users from the house
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult deleteHouse(int id)
        {
            var house = dbmsContext.houses.Find(id);
            if (house == null)
            {
                return NotFound("House not found");
            }

            // Find all users of house
            var occupants = dbmsContext.roomates.Where(e => e.houseId == id).ToList();

            // Remove them from house
            foreach (var roomate in occupants)
            {
                roomate.houseId = null;
            }

            // Delete House
            dbmsContext.houses.Remove(house);

            dbmsContext.SaveChanges();

            return Ok();
        }

        [HttpGet("ingredients/{houseId}")]
        public async Task<IActionResult> getHouseIngredients(int houseId)
        {
            var roomates = dbmsContext.roomates.Where(x => x.houseId == houseId)
                .ToList();

            var roomateIds = roomates.ConvertAll(x => x.roomateId);

            var roomateIngredients = dbmsContext.roomateIngredients.Where(x => roomateIds.Contains(x.roomateId)).OrderBy(x => x.ingredientId);

            var ingredientsResp = new List<IngredientResponseModel>(roomateIngredients.Count());
            foreach (var roomateIngredient in roomateIngredients)
            {
                var ingredient = dbmsContext.ingredients.Find(roomateIngredient.ingredientId);
                if (ingredient == null)
                {
                    return NotFound("Invalid Roomate Ingredients");
                }

                ingredientsResp.Add(IngredientResponseModel.ingredientResponseBuilder(ingredient, roomateIngredient, roomateIngredient.roomate));
            }

            return Ok(ingredientsResp);
        }

        // Most likely their is a smarter way to do this but I'm not
        // sure of a way to compare two lists within sql. So we get all recipes that we have an ingredient for
        // And then remove the ones that they don't have everything. This could be modified to sort by owned ingredients
        [HttpGet("possiblerecipes/{id}")]
        public async Task<IActionResult> getPossibleRecipes(int id)
        {
            var roomates = dbmsContext.roomates.Where(x => x.houseId == id).Select(x => x.roomateId).ToList();

            if (roomates.Count() == 0)
            {
                return NotFound();
            }

            // Get all recipes that we have at least one ingredient
            var roomateIngredients = dbmsContext.roomateIngredients
                .Where(x => roomates.Contains(x.roomateId));

            var recipeIngredients = roomateIngredients
                .Join(dbmsContext.recipeIngredients, ri => ri.ingredientId, r => r.ingredientId, (ri, r) => r.recipeId);

            // Get all recipes from our recipe ingredients
            var recipes = dbmsContext.recipes.Where(x => recipeIngredients.Contains(x.recipeId));

            // Create object to store response
            var responseObject = new List<object>();

            // Get list of roomate ingredient ids
            var ingredientIds = roomateIngredients.Select(x => x.ingredientId);

            foreach (var recipe in recipes)
            {
                // Load recipe ingredients
                dbmsContext.Entry(recipe).Collection(r => r.ingredients).Load();

                var ingredientCount = 0;

                // Find out how many matching ingredients they have
                foreach (var ingredient in recipe.ingredients)
                {
                    if (ingredientIds.Contains(ingredient.ingredientId))
                    {
                        ingredientCount++;
                    }
                }

                // If they have all ingredients add to response
                if (ingredientCount == recipe.ingredients.Count())
                {
                    responseObject.Add(new { 
                        recipeId = recipe.recipeId,
                        recipeName = recipe.recipeName,
                        recipeDescription = recipe.description
                    });
                }
            }

            return Ok(responseObject);
        }
    }
}
