using DBMSApi.Controllers.Viewmodels;
using DBMSApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBMSApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/roomate")]
    public class RoomateController : ControllerBase
    {
        DBMSContext _db;
        public RoomateController(DBMSContext db)
        {
            _db = db;
        }

        [HttpGet]
        public List<Roomate> Index()
        {
            return _db.roomates.ToList();
        }

        [HttpPut]
        public IActionResult createRoomate([FromBody] CreateRoomateViewModel data)
        {
            if (data.houseId != null)
            {
                if (_db.houses.Find(data.houseId) == null)
                {
                    return NotFound("House not found");
                }
            }

            var roomate = new Roomate()
            {
                username = data.username,
                isOwner = false,
                houseId = data.houseId
            };

            _db.roomates.Add(roomate);
            _db.SaveChanges();

            return Ok(roomate);
        }

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
        [HttpPost("/addIngredient/{id}")]
        public IActionResult addIngredient(int id, [FromBody] AddIngredientRoomateViewModel data)
        {
            // verify user
            var roomate = _db.roomates.Find(id);

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
                ingredientId = data.ingredientId,
                roomateId = id,
                quantity = data.amount,
                quantityUnit = data.amountUnit,
                price = data.price,
                priceUnit = data.priceUnit,
                expiredDate = data.expiredDate
            };

            _db.roomateIngredients.Add(roomateIngredient);
            _db.SaveChanges();

            return Ok(roomate);
        }

        [HttpDelete("/removeingredient/{roomateId}/{ingredientId}")]
        public IActionResult removeIngredient(int roomateId, int ingredientId)
        {
            var roomateIngredient = _db.roomateIngredients.Find(roomateId, ingredientId);

            if (roomateIngredient == null)
            {
                return NotFound("Unable to find your ingredient");
            }

            _db.roomateIngredients.Remove(roomateIngredient);
            _db.SaveChanges();
            return Ok();
        }
    }
}
