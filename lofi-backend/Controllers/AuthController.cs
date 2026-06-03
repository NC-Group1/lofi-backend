using lofi_backend.Data_Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Supabase;
using Supabase.Gotrue;

namespace lofi_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private Supabase.Client _supabaseClient;

        public AuthController(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            if(request == null || string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return BadRequest(new { message = "New password cannot be empty." });
            }
            try
            {
                var attributes = new Supabase.Gotrue.UserAttributes
                {
                    Password = request.NewPassword
                };
                var updatedUserPassword = await _supabaseClient.Auth.Update(attributes);
                if (updatedUserPassword != null)
                {
                    Console.WriteLine("Password updated successfully.");
                    return Ok(new { message = "Password updated successfully." });
                }
                return BadRequest(new { message = "Failed to update password." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending password update email: {ex.Message}");
                return StatusCode(500, new { message = "Error updating password." });
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            // email will be the raw JSON string, e.g. "example@gmail.com"
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return BadRequest("Email is required.");
                }

                Console.WriteLine($"Received email for password reset: {email}");

                var options = new ResetPasswordForEmailOptions(email)
                {
                    RedirectTo = "https://localhost:5082/Login"
                };

                await _supabaseClient.Auth.ResetPasswordForEmail(options);

                Console.WriteLine("Password reset email sent successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending password reset email: {ex.Message}");
                return BadRequest("Error sending password reset email.");
            }
        }

        public class UpdatePasswordRequest
        {
            public string NewPassword { get; set; } = string.Empty;
        }
    }
}
