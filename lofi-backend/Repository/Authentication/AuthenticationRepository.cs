using System.Text.Json;
using Castle.Components.DictionaryAdapter;
using lofi_backend;
using lofi_backend.Data_Models;
using Microsoft.Extensions.Options;
using Supabase;

namespace lofi_backend.Repository.Authentication
{
    public interface IAuthenticationRepository
    {
        public Task<AuthToken> SignInAsync(string email, string password);
        public Task<AuthToken> SignUpAsync(string email, string password);
    }
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private Client _supabaseClient;

        public AuthenticationRepository(Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<AuthToken> SignInAsync(string email, string password)
        {
            try
            {
                Console.WriteLine("Getting user from supabase");
                var session = await _supabaseClient.Auth.SignIn(email, password);

                Console.WriteLine(session?.User);
                Console.WriteLine(session?.User?.Id);
                
                var authToken = new AuthToken
                (
                    id: session?.User?.Id?.ToString()!,
                    accessToken: session?.AccessToken!,
                    refreshToken: session?.RefreshToken!,
                    expiresIn: session?.ExpiresIn.ToString()!,
                    expiresAt: session?.ExpiresAt().ToString()!,
                    tokenType: session?.TokenType!
                );

                return authToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<AuthToken> SignUpAsync(string email, string password)
        {
            try
            {
                Console.WriteLine("Creating user in supabase");
                var session = await _supabaseClient.Auth.SignUp(email, password);
                Console.WriteLine(session?.User?.Email);
                Console.WriteLine(session?.User?.Id);
                var authToken = new AuthToken
                (
                    id: session?.User?.Id,
                    accessToken: session?.AccessToken,
                    refreshToken: session?.RefreshToken,
                    expiresIn: session?.ExpiresIn.ToString(),
                    expiresAt: session?.ExpiresAt().ToString(),
                    tokenType: session?.TokenType
                );                
                Console.WriteLine(authToken.Id);

                return authToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during sign-up: {ex.Message}");
                throw;
            }
        }
    }
}
