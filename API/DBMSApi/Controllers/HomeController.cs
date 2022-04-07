using DBMSApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBMSApi.Controllers
{
    [ApiController]
    [Route("/api/house")]
    public class HomeController : ControllerBase
    {
        DBMSContext dbmsContext;
        public HomeController(DBMSContext db)
        {
            dbmsContext = db;
        }

        [HttpGet]
        public List<House> Index()
        {
            return dbmsContext.houses.ToList();
        }

        [HttpPut]
        public House createHouse()
        {
            var house = new House() { houseName = "Test House", ownerId = 1234 };
            dbmsContext.houses.Add(house);
            dbmsContext.SaveChanges();
            return house;
        }
    }
}
