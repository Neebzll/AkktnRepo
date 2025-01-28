
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AKKTN_Pr00.ViewModel
{
    public class Sign_in_ViewModel
    {
        [Required(ErrorMessage = "EmailAddress 1 is required")]
   
        public string EmailAddress1 { get; set; }
        [Required(ErrorMessage = "company password is required")]
        [DataType(DataType.Password)]
        public string companypassword { get; set; }

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }


    }
}
