using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace AKKTN_Pr00.ViewModel
{
    public class Sign_UpViewModel
    {
        [Required(ErrorMessage = "CompanyName is required")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "companypass is required")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
        [DataType(DataType.Password)]
        public string companypass { get; set; }
        [Required(ErrorMessage = "Registration number is required")]
        
        public string RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public string status { get; set; }
        [Required(ErrorMessage = "ContactName1 is required")]
        [Compare("ContactName2", ErrorMessage = "ContactName does not match.")]
        public string ContactName1​ { get; set; }
        [Required(ErrorMessage = "Email1 is required")]
        [Compare("Email2", ErrorMessage = "Email does not match.")]
        public string Email1​ { get; set; }
        [Required(ErrorMessage = "Cell1 is required")]
        [Compare("Cell2", ErrorMessage = "Cell does not match.")]
        public string Cell1 { get; set; }
        [Required(ErrorMessage = "ContactName2 is required")]
        public string ContactName2 { get; set; }

        [Required(ErrorMessage = "Email2 is required")]
        public string Email2 { get; set; }
        [Required(ErrorMessage = "Cell2 is required")]
        public string Cell2 { get; set; }

        public string CompanyId { get; private set; }

        public void GenerateCompanyId()
        {
            if (!string.IsNullOrWhiteSpace(CompanyName) && !string.IsNullOrWhiteSpace(Cell1))
            {
                CompanyId = GenerateCompanyId(CompanyName, Cell1);
            }
        }

        // Static method to generate Company ID
        private static string GenerateCompanyId(string companyName, string cell1)
        {
            // Combine the company name and cell
            string combined = $"{companyName.Trim().ToLower()}-{cell1.Trim()}";

            // Generate a hash of the combined string
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                // Convert the byte array to a hexadecimal string
                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }

                // Return the first 10 characters of the hash as the Company ID
                return hashString.ToString().Substring(0, 10).ToUpper();
            }
        }

    }
}
