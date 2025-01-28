using Microsoft.AspNetCore.Identity;

namespace AKKTN_Pr00.Models
{
    public class user : IdentityUser
    {
        public string FullName { get; set; }
    }
}
