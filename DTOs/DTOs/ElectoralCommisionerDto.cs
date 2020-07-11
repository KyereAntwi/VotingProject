using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs.DTOs
{
    public class ElectoralCommisionerDto
    {
        [Key]
        public Guid Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

        public Guid PollDtoId { get; set; }
        [ForeignKey(nameof(PollDtoId))]
        public PollDto PollDto { get; set; }
    }
}
