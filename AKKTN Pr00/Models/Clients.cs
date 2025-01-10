using System;
using System.ComponentModel.DataAnnotations;
namespace AKKTN_Pr00.Models
{
    public class Clients
    {
        public int ClientID { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        [StringLength(150, ErrorMessage = "Company ID cannot exceed 150 characters")]
        public string CompanyID { get; set; }

        [Required(ErrorMessage = "Registration Number is required")]
        [StringLength(50, ErrorMessage = "Registration Number cannot exceed 50 characters")]
        public string RegistrationNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "CIPC Registration Date")]
        public DateTime? CIPCRegistrationDate { get; set; }

        [StringLength(50, ErrorMessage = "Income Tax Number cannot exceed 50 characters")]
        public string IncomeTaxNumber { get; set; }

        public bool VAT { get; set; }

        [StringLength(80, ErrorMessage = "VAT Period cannot exceed 80 characters")]
        [Display(Name = "VAT Period")]
        public string VATPeriod { get; set; }

        public bool PayeeNumber { get; set; }

        [StringLength(50, ErrorMessage = "Payee Reference Number cannot exceed 50 characters")]
        [Display(Name = "Payee Reference Number")]
        public string PayeeReferenceNumber { get; set; }

        public bool EMP501 { get; set; }

        [StringLength(50, ErrorMessage = "EMP501 Reference Number cannot exceed 50 characters")]
        [Display(Name = "EMP501 Reference Number")]
        public string EMP501ReferenceNumber { get; set; }

        public bool UIF { get; set; }

        [StringLength(50, ErrorMessage = "UIF Number cannot exceed 50 characters")]
        [Display(Name = "UIF Number")]
        public string UIFNumber { get; set; }

        public bool WCC { get; set; }

        [StringLength(50, ErrorMessage = "WCC Number cannot exceed 50 characters")]
        [Display(Name = "WCC Number")]
        public string WCCNumber { get; set; }

        public bool Payroll { get; set; }
        public bool MonthlyCashbook { get; set; }
        public bool FinancialStatements { get; set; }
        public bool IncomeTaxReturn { get; set; }
    }
}
