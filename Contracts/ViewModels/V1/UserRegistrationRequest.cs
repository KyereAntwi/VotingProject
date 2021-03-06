﻿using System.ComponentModel.DataAnnotations;

namespace Contracts.ViewModels.V1
{
    public class UserRegistrationRequest
    {
        [Required]
        [StringLength(15, MinimumLength = 6)]
        public string Username { get; set; }
        [StringLength(15,MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
