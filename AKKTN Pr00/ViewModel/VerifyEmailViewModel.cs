using System.ComponentModel.DataAnnotations;

namespace AKKTN_Pr00.ViewModel
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}
