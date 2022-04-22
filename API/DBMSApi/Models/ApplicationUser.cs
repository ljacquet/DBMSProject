using Microsoft.AspNetCore.Identity;

namespace DBMSApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int roomateId { get; set; }

        public virtual Roomate roomate { get; set; }
    }
}
