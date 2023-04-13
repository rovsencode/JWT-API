using Microsoft.AspNetCore.Identity;

namespace P326FirstWebAPI.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
