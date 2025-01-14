using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace AKKTN_Pr00.ViewModel
{
    public class Sign_UpViewModel
    {
        [Required(ErrorMessage = "CompanyName is required")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "company password is required")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} characters long.")]
        [Compare("Confirm company password", ErrorMessage = "company password does not match.")]
        [DataType(DataType.Password)]
        public string Company password { get; set; }

        [Required(ErrorMessage = "confirm company password is required")]
        public string Confirm company password { get; set; }

        [Required(ErrorMessage = "Registration number is required")]
        public string RegistrationNumber { get; set; }

       [Required(ErrorMessage = "Status is required")]
        public string status { get; set; }

        [Required(ErrorMessage = "ContactName1 is required")]
        [Compare("ContactName2", ErrorMessage = "ContactName does not match.")]
        public string ContactName1​ { get; set; }

        [Required(ErrorMessage = "Email address 1 is required")]
        [Compare("Email Adress 2", ErrorMessage = "Email adress does not match.")]
        public string Email Adress1​ { get; set; }

        [Required(ErrorMessage = "Cellphone1 is required")]
        [Compare("Cellphone 2", ErrorMessage = "Cellphone does not match.")]
        public string Cellphone 1 { get; set; }

        [Required(ErrorMessage = "ContactName 2 is required")]
        public string ContactName 2 { get; set; }

        [Required(ErrorMessage = "Email Address 2 is required")]
        public string Email Address2 { get; set; }
        [Required(ErrorMessage = "Cellphone2 is required")]
        public string Cellphone2 { get; set; }

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
