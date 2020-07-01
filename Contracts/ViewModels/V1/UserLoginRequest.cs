using System.ComponentModel.DataAnnotations;

namespace Contracts.ViewModels.V1
{
    public class UserLoginRequest
    {
        [Required]
        [StringLength(15, MinimumLength = 6)]
        public string Username { get; set; }
        [StringLength(15, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
