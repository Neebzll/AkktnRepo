using Google.Apis.Admin.Directory.directory_v1.Data;
using System.ComponentModel.DataAnnotations;

namespace Login_and_Registration.ViewModel
{
    public class Sign_UpViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match.")]
        public string Password​ { get; set; }
        [Required(ErrorMessage = "ConfirmedPassword is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "CompanyName is required")]
        public string CompanyName​ { get; set; }
        [Required(ErrorMessage = "Registration number is required")]
        public string RegistrationNumber​ { get; set; }
        [Required(ErrorMessage = "ContactName is required")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Cellphone is required")]
        public string Cellphonenumber​ { get; set; }


    }
}
