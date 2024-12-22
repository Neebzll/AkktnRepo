using System.ComponentModel.DataAnnotations;

namespace Login_and_Registration.Models
{
    public class assignedTasks
    {
        [Key]
        public int assignedID { get; set; }
        
        [Required]
        public int ClientID { get; set; }

        [Required]
        public int TaskID { get; set; }

        [Required]
        public int memberID { get; set; }
    }
}
