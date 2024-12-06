using System.ComponentModel.DataAnnotations;

namespace AKKTN_Pr00.Models
{
    public class Tasks
    {
        [Key]
        public int TaskID {get; set;}

        [StringLength(150, ErrorMessage = "Company ID must be under 150 characters")]
        public string CompanyID { get; set; }

        [Required]
        [StringLength(180,ErrorMessage ="Task Description can't be more than 180 characters")]
        public string TaskDescription {  get; set; }

        [Required]
        public DateTime? AssignTaskDate { get; set; }

        [Required]
        public DateTime? DueDate { get; set; }
        
        [Required]
        public string Reminders {  get; set; }

        [Required]
        public string TaskStatus { get; set; }
    }
}
