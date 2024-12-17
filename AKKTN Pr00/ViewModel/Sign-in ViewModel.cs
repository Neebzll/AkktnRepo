using Google.Apis.Admin.Directory.directory_v1.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Login_and_Registration.ViewModel
{
    public class Sign_in_ViewModel
    {
        [Required(ErrorMessage = "Email is required")]
   
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }


    }
}
