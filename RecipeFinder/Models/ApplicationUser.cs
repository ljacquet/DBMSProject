using Microsoft.AspNetCore.Identity;

namespace RecipeFinder.Models
{
    public class ApplicationUser : IdentityUser
    {
        public uint? RoomateId { get; set; }
    }
}