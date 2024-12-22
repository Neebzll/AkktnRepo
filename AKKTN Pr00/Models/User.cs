using Microsoft.AspNetCore.Identity;

namespace Login_and_Registration.Models
{
    public class user : IdentityUser
    {
        public string FullName { get; set; }
    }
}
