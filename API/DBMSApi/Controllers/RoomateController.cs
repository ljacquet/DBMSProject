using DBMSApi.Controllers.Viewmodels;
using DBMSApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DBMSApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/roomate")]
    public class RoomateController : ControllerBase
    {
        DBMSContext _db;
        private readonly UserManager<ApplicationUser> userManager;

        public RoomateController(DBMSContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            this.userManager = userManager;
        }

        [HttpGet]
        public List<Roomate> Index()
        {
            return _db.roomates.ToList();
        }

        [HttpGet("{id}")]
        public Roomate getRoomate(int id)
        {
            return _db.roomates.Find(id);
        }

        // Obsolete roomates now created on account creation
        //[HttpPut]
        //public IActionResult createRoomate([FromBody] CreateRoomateViewModel data)
        //{
        //    if (data.houseId != null)
        //    {
        //        if (_db.houses.Find(data.houseId) == null)
        //        {
        //            return NotFound("House not found");
        //        }
        //    }

        //    var roomate = new Roomate()
        //    {
        //        username = data.username,
        //        isOwner = false,
        //        houseId = data.houseId
        //    };

        //    _db.roomates.Add(roomate);
        //    _db.SaveChanges();

        //    return Ok(roomate);
        //}

        [HttpDelete("{id}")]
        public IActionResult deleteRoomate(int id)
        {
            var roomate = _db.roomates.Find(id);

            if (roomate == null)
            {
                return NotFound("Roomate not found");
            }

            _db.roomates.Remove(roomate);
            return Ok();
        }

        //Add ingredients
        [HttpPost("addingredient")]
        public async Task<IActionResult> addIngredient([FromBody] AddIngredientRoomateViewModel data)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return NotFound();

            // verify user
            var roomate = _db.roomates.Find(user.roomateId);

            if (roomate == null)
            {
                return NotFound("Unable to find roomate");
            }

            // verify ingredient
            var ingredient = _db.ingredients.Find(data.ingredientId);

            if (ingredient == null)
            {
                return NotFound("Unable to find ingredient");
            }

            var roomateIngredient = new RoomateIngredient()
            {
                roomate = roomate,
                ingredient = ingredient,
                quantity = data.amount,
                quantityUnit = data.amountUnit,
                price = data.price
            };

            //roomate.roomateIngredients.Add(roomateIngredient);
            //ingredient.roomateIngredients.Add(roomateIngredient);

            // Track all entities so EF Core knows to update them
            //_db.roomates.Attach(roomate);
            //_db.ingredients.Attach(ingredient);

            _db.roomateIngredients.Add(roomateIngredient);

            _db.SaveChanges();

            return Ok();
        }

        [HttpPost("updateIngredient/{ingredientId}")]
        public async Task<IActionResult> updateIngredient(int ingredientId, [FromBody] UpdateIngredientRoomateModel model)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return NotFound();

            // verify user
            var roomate = _db.roomates.Find(user.roomateId);

            if (roomate == null)
            {
                return NotFound("Unable to find roomate");
            }

            var connector = _db.roomateIngredients.Where(x => x.ingredientId == ingredientId && x.roomateId == roomate.roomateId).FirstOrDefault();
            if (connector == null)
            {
                return NotFound("Unable to find ingredient");
            }

            connector.price = model.price;
            connector.quantity = model.amount;
            connector.quantityUnit = model.amountUnit;

            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete("removeingredient/{ingredientId}")]
        public async Task<IActionResult> removeIngredient(int ingredientId)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return NotFound();

            // verify user
            var roomate = _db.roomates.Find(user.roomateId);

            if (roomate == null)
            {
                return NotFound("Unable to find roomate");
            }

            var roomateIngredient = _db.roomateIngredients.Where(x => x.ingredientId == ingredientId && x.roomateId == roomate.roomateId).FirstOrDefault();//_db.roomateIngredients.Find(roomate.roomateId, ingredientId);

            if (roomateIngredient == null)
            {
                return NotFound("Unable to find your ingredient");
            }

            _db.roomateIngredients.Remove(roomateIngredient);
            _db.SaveChanges();



            return Ok();
        }

        [HttpGet("ingredients")]
        public async Task<IActionResult> getUserIngredients()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return NotFound();

            var roomate = _db.roomates.Find(user.roomateId);

            var roomateIngredients = _db.roomateIngredients.Where(x => x.roomateId == roomate.roomateId).ToList();

            if (roomateIngredients == null)
            {
                // If no ingredients return empty array
                return Ok(Array.Empty<object>());
            }
            else
            {
                // Build response array because returning directly creates cyclical response
                var responseArray = new List<IngredientResponseModel>();

                foreach (var roomateIngredient in roomateIngredients)
                {
                    var ingredient = _db.ingredients.Find(roomateIngredient.ingredientId);
                    if (ingredient == null)
                    {
                        return NotFound("User Ingredients List Bad");
                    }

                    responseArray.Add(IngredientResponseModel.ingredientResponseBuilder(ingredient, roomateIngredient, roomateIngredient.roomate));
                }

                return Ok(responseArray);
            }
        }

        [HttpGet("ingredients/{id}")]
        public async Task<IActionResult> getUserIngredients(int id) { 

            var roomate = _db.roomates.Find(id);

            var roomateIngredients = _db.roomateIngredients.Where(x => x.roomateId == roomate.roomateId).ToList();

            if (roomateIngredients == null)
            {
                // If no ingredients return empty array
                return Ok(Array.Empty<object>());
            }
            else
            {
                // Build response array because returning directly creates cyclical response
                var responseArray = new List<IngredientResponseModel>();

                foreach (var roomateIngredient in roomateIngredients)
                {
                    var ingredient = _db.ingredients.Find(roomateIngredient.ingredientId);
                    if (ingredient == null)
                    {
                        return NotFound("User Ingredients List Bad");
                    }

                    responseArray.Add(IngredientResponseModel.ingredientResponseBuilder(ingredient, roomateIngredient, roomateIngredient.roomate));
                }

                return Ok(responseArray);
            }
        }
    }
}
