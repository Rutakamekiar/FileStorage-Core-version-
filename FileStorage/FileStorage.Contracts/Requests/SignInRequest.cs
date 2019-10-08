using System.ComponentModel.DataAnnotations;

namespace FileStorage.Contracts.Requests
{
    public class SignInRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
