using lofi_backend.Data_Models;
using lofi_backend.Service;
using Microsoft.AspNetCore.Authentication;
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
        private readonly Client _supabaseClient = supabaseClient;

        public AuthController(Supabase.Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(UserWithPassword user)
        {
            try
            {
                Console.WriteLine("Getting user from supabase");
                Console.WriteLine($"User Email: {user.UserData.Email}");
                Console.WriteLine($"User Password: {user.Password}");
                var session = await _supabaseClient.Auth.SignUp(user.UserData.Email, user.Password) 
                    ?? throw new UnauthorizedAccessException("Invalid credentials");

                if (session.AccessToken == null) throw new UnauthorizedAccessException("Invalid Credentials");
                if (session.RefreshToken == null) throw new UnauthorizedAccessException("Invalid Credentials");

                Console.WriteLine(session?.User);
                Console.WriteLine(session?.User?.Id);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = session?.ExpiresAt()
                };

                Response.Cookies.Append("supabase_jwt", session!.AccessToken, cookieOptions);
                Response.Cookies.Append("supabase_refresh_token", session!.RefreshToken, cookieOptions);

                return Created();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Unauthorized();
            }
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(UserWithPassword user)
        {
            try
            {
                Console.WriteLine("Getting user from supabase");
                var session = await _supabaseClient.Auth.SignInWithPassword(user.UserData.Email, user.Password)
                    ?? throw new UnauthorizedAccessException("Invalid credentials");

                if (session.AccessToken == null) throw new UnauthorizedAccessException("Invalid Credentials");
                if (session.RefreshToken == null) throw new UnauthorizedAccessException("Invalid Credentials");

                Console.WriteLine(session?.User);
                Console.WriteLine(session?.User?.Id);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = session?.ExpiresAt()
                };

                Response.Cookies.Append("supabase_jwt", session!.AccessToken, cookieOptions);
                Response.Cookies.Append("supabase_refresh_token", session!.RefreshToken, cookieOptions);
                return Ok("User has been signed in successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpPost("sign-out")]
        public async Task<ActionResult> SignOut(UserData user)
        {
            Response.Cookies.Delete("supabase_jwt");
            Response.Cookies.Delete("supabase_refresh");
            return Ok(new { message = "logged out" });
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
