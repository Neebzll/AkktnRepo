
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Login_and_Registration.ViewModel
{
    public class Sign_in_ViewModel
    {
        [Required(ErrorMessage = "Email1 is required")]
   
        public string Email1 { get; set; }
        [Required(ErrorMessage = "companypass is required")]
        [DataType(DataType.Password)]
        public string companypass { get; set; }

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }


    }
}
