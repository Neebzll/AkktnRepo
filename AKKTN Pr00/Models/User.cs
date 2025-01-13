using Microsoft.AspNetCore.Identity;

namespace Login_and_Registration.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
