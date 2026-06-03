using System.ComponentModel.DataAnnotations;

namespace lofi_backend.Data_Models
{
    public class ResetPasswordForm
    {
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;
    }
}
