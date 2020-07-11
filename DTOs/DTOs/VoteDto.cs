using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs.DTOs
{
    public class VoteDto
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

        public Guid NomineeDtoId { get; set; }
        [ForeignKey(nameof(NomineeDtoId))]
        public NomineeDto NomineeDto { get; set; }

        public Guid CategoryDtoId { get; set; }
        [ForeignKey(nameof(CategoryDtoId))]
        public CategoryDto CategoryDto { get; set; }
    }
}
