using System.ComponentModel.DataAnnotations;

namespace Models.Account
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W)[a-zA-Z0-9\W]{6,}$", ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase,  1 number, 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; }
    }
}
