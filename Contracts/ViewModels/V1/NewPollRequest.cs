using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.ViewModels.V1
{
    public class NewPollRequest
    {
        [Required]
        public string Theme { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartedDateTime { get; set; }
        [Required]
        public DateTime EndedDateTime { get; set; }
    }
}
