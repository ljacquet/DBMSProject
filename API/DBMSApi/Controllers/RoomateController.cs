using Microsoft.AspNetCore.Mvc;

namespace DBMSApi.Controllers
{
    [ApiController]
    [Route("/api/roomate")]
    public class RoomateController : ControllerBase
    {
        DBMSContext _db;
        public RoomateController(DBMSContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
