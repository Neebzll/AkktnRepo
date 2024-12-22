using System.ComponentModel.DataAnnotations;

namespace Login_and_Registration.Models
{
    public class Clients
    {
        [Key]
        public int ClientID { get; set; }
     
        public string CompanyID { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Name must be under 150 characters")]
        public string ClientName { get; set; }
        [Required]
        [StringLength(13, ErrorMessage = "Registration number must be under 13 characters")]
        public string RegistrationNumber { get; set; }
        
        [Required]
        public DateTime? CIPCRegistrationDate { get; set; }
        
        [StringLength(10,ErrorMessage ="Income tax number should only be 10 digits")]
        public string IncomeTaxNumber { get; set; }

        
        public bool VAT {  get; set; }
        public string? VATPeriod {  get; set; }
        
      
        public bool PayeeNumber { get; set; }
        [StringLength(10, ErrorMessage = "Payee Reference number should only be 10 digits")]
        public string? PayeeReferenceNumber {  get; set; }

        public bool EMP501 { get; set; }

       
        public bool UIF { get; set; }
        [StringLength(9,ErrorMessage ="UIF Number is 9 digits")]
        public string? UIFNumber { get; set; }

        public bool WCC { get; set; }
        [StringLength(10, ErrorMessage = "WCC number is 10 characters")]
        public string? WCCNumber { get; set; }

        public bool Payroll {  get; set; }
        
     
        public bool MonthlyCashbook {  get; set; }
     
        public bool FinancialStatements { get; set; }

        public bool IncomeTaxReturn {  get; set; }

    }
}
