using System.ComponentModel.DataAnnotations;

namespace AKKTN_Pr00.Models
{
    public class Tasks
    {
        [Key]
        public int TaskID { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        [StringLength(150, ErrorMessage = "Company ID cannot exceed 150 characters")]
        public string CompanyID { get; set; }

        [Required(ErrorMessage = "Task Description is required")]
        [StringLength(255, ErrorMessage = "Task Description cannot exceed 255 characters")]
        public string TaskDescription { get; set; }

        [Required(ErrorMessage = "Assign Date is required")]
        [DataType(DataType.Date)]
        public DateTime AssignTaskDate { get; set; }

        [Required(ErrorMessage = "Due Date is required")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Task Status is required")]
        [StringLength(20, ErrorMessage = "Task Status cannot exceed 20 characters")]
        public string TaskStatus { get; set; }

        [StringLength(50, ErrorMessage = "Reminders cannot exceed 50 characters")]
        public string Reminders { get; set; }
    }
}
