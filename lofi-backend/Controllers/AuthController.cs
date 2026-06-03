using lofi_backend.Data_Models;
using lofi_backend.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Supabase;

namespace lofi_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(Client supabaseClient) : ControllerBase
    {
        private readonly Client _supabaseClient = supabaseClient;

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
    }
}
