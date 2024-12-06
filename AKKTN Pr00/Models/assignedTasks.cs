using System.ComponentModel.DataAnnotations;

namespace AKKTN_Pr00.Models
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
