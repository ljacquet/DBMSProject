using DBMSApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBMSApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/house")]
    public class HomeController : ControllerBase
    {
        DBMSContext dbmsContext;
        public HomeController(DBMSContext db)
        {
            dbmsContext = db;
        }

        /// <summary>
        /// Lists Houses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<House> Index()
        {
            return dbmsContext.houses.ToList();
        }

        /// <summary>
        /// Creates house and assigns it to user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="houseName"></param>
        /// <returns>Created House Object</returns>
        [HttpPut("{id}")]
        public IActionResult createHouse(int id, [FromBody] string houseName)
        {
            // Check Person Exists
            var owner = dbmsContext.roomates.Find(id);

            if (owner == null)
            {
                return NotFound();
            }

            var house = new House() { houseName = houseName, ownerId = id };
            dbmsContext.houses.Add(house);

            owner.isOwner = true;

            dbmsContext.SaveChanges();
            return Ok(house);
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
    }
}
