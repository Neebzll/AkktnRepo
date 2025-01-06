
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AKKTN_Pr00.ViewModel
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
