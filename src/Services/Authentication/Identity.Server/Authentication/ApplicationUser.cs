using Microsoft.AspNetCore.Identity;

namespace PingDong.Newmoon.IdentityServer.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
