using System.ComponentModel.DataAnnotations;

namespace AKKTN_Pr00.Models
{
    public class CompanyTeam
    {
        [Key]
        public int memberID {  get; set; }
        
        [StringLength(150, ErrorMessage = "Company ID must be under 150 characters")]
        public string CompanyID { get; set; }

        [StringLength(80,ErrorMessage ="Company name must be not more than 80 characters")]
        public string ContactName {  get; set; }
        
        [Required]
        [StringLength(100,ErrorMessage ="JobTitle can't be more than 100 characters")]
        public string JobTitle {  get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "email address must be under 150")]
        public string Email { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "Cellphone Number must at least be under 11 characters")]
        public string Cell { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "Role can't be more than 80 characters")]
        public string Role {  get; set; }
    }
}
