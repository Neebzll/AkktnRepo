//using System.ComponentModel.DataAnnotations;

//namespace AKKTN_Pr00.Models
//{
//    public class Company
//    {
//        [Key]
//        [StringLength(150,ErrorMessage ="Company ID must be under 150 characters")]
//        public string CompanyID { get; set; }

//        [Required]
//        [StringLength(150,ErrorMessage ="Name must be under 150 characters")]
//        public string CompanyName { get; set; }
//        [Required]
//        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$",
//    ErrorMessage = "Password must be at least 10 characters long and contain at least one uppercase letter, one number, and one special character.")]
//        public string companypass { get; set; }

//        [Required]
//        [StringLength(14,ErrorMessage ="Registration number must be under 13 characters")]
//        public string RegistrationNumber { get; set; }

//        //[Required]
//        public string Status { get; set; }

//        [Required]
//        [StringLength(80,ErrorMessage ="Contact name must be under 80 characters")]
//        public string ContactName1 { get; set; }

//        [Required]
//        [StringLength(150,ErrorMessage ="email address must be under 150")]
//        public string Email1 { get; set; }

//        [Required]
//        [StringLength(11,ErrorMessage ="Cellphone Number must at least be under 11 characters")]
//        public string Cell1 { get; set; }        


//        [StringLength(80,ErrorMessage ="Contact name must be under 80 characters")]
//        public string? ContactName2 { get; set; }

//        [StringLength(150,ErrorMessage ="email address must be under 150")]
//        public string? Email2 { get; set; }

//        [StringLength(11,ErrorMessage ="Cellphone Number must at least be under 11 characters")]
//        public string? Cell2 { get; set; }


//    }
//}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Required for [NotMapped]

namespace AKKTN_Pr00.Models
{
    public class Company
    {
        [Key]
        [StringLength(150, ErrorMessage = "Company ID must be under 150 characters")]
        public string CompanyID { get; set; }

       
        [StringLength(150, ErrorMessage = "Name must be under 150 characters")]
        public string CompanyName { get; set; }

        // This remains stored in the database but is NOT displayed in the form
        public string? companypass { get; set; }

        // This field is only used for updates (NOT stored in the database)
        [NotMapped]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$",
            ErrorMessage = "Password must be at least 10 characters long and contain at least one uppercase letter, one number, and one special character.")]
        public string? NewPassword { get; set; } // Optional: Only for updates

        [Required]
        [StringLength(14, ErrorMessage = "Registration number must be under 13 characters")]
        public string RegistrationNumber { get; set; }

        public string Status { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "Contact name must be under 80 characters")]
        public string ContactName1 { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Email address must be under 150 characters")]
        public string Email1 { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "Cellphone Number must at least be under 11 characters")]
        public string Cell1 { get; set; }

        [StringLength(80, ErrorMessage = "Contact name must be under 80 characters")]
        public string? ContactName2 { get; set; }

        [StringLength(150, ErrorMessage = "Email address must be under 150 characters")]
        public string? Email2 { get; set; }

        [StringLength(11, ErrorMessage = "Cellphone Number must at least be under 11 characters")]
        public string? Cell2 { get; set; }
    }
}

