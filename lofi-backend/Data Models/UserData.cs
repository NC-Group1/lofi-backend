 using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Azure.Core;
using Azure.Identity;
using lofi_backend.Data_Models.Enums;

namespace lofi_backend.Data_Models
{
    public class UserData
    {
        public UserData() { }

        public UserData(string id, string username, string firstName, string lastName, string email, DateTime dateOfBirth, Gender gender)
        {
            Id = id;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }

        public string Id { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } 
        [Required]
        public string FirstName { get; set; } 
        [Required]
        public string LastName { get; set; } 
        [Required]
        public string Email { get; set; } 
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Gender Gender { get; set; } 
        public List<Playlist> Playlists { get; set; } = new List<Playlist>();
    }

    public class AuthToken    
    {
        public AuthToken() { }

        public AuthToken(string id, string accessToken, string tokenType, string refreshToken, string expiresIn, string expiresAt)
        {
            Id = id;
            AccessToken = accessToken;
            TokenType = tokenType;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
            ExpiresAt = expiresAt;
        }

        public string Id { get; set; }
        public string AccessToken {  get; set; } 
        public string TokenType { get; set; } 
        public string RefreshToken { get; set; }
        public string ExpiresIn { get; set; }        
        public string ExpiresAt { get; set; }
    }

    public class AuthenticatedUser
    {
        public AuthenticatedUser() { }

        public AuthenticatedUser(UserData user, AuthToken authToken)
        {
            UserData = user;
            AuthToken = authToken;
        }

        public UserData UserData { get; set; }
        public AuthToken AuthToken { get; set; }
    }

    public class  UserWithPassword
    {
        public UserWithPassword() { }

        public UserWithPassword(UserData user, string password)
        {
            UserData = user;
            Password = password;
        }

        public UserData UserData { get; set; }
        public string Password { get; set; }
    }
}
