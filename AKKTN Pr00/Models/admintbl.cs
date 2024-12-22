using System.ComponentModel.DataAnnotations;

namespace Login_and_Registration.Models;

public class admintbl
{
   [Key]
   public int adminID {  get; set; }
   [Required]
    [StringLength(150,ErrorMessage ="email must be less than 150 characters")]
   public string email {  get; set; }
    [Required]
    [StringLength(11,ErrorMessage ="cellphone must not be more than 11 digits")]
    public string cell {  get; set; }

    [Required]
    [StringLength(90, ErrorMessage = "password must be less than 90 characters")]
    public string adminpass{ get; set; }
}
