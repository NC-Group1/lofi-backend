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

        [HttpPost]
        public async Task<IActionResult> ForwardPassword([FromBody] string email)
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
    }
}
