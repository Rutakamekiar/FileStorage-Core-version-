using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class RegisterBindingModel
    {
        [Required] public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Password must be > 6", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}