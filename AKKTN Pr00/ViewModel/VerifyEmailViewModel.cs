using System.ComponentModel.DataAnnotations;

namespace Login_and_Registration.ViewModel
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}
