using System.ComponentModel.DataAnnotations;

namespace FileStorage.WebApi.Models
{
    public class RegisterBindingModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}