using Microsoft.AspNetCore.Identity;

namespace PingDong.Newmoon.Identity.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
