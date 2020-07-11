using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs.DTOs
{
    public class CategoryDto
    {
        [Key]
        public Guid Id { get; set; }
        public string Theme { get; set; }

        public Guid PollDtoId { get; set; }
        [ForeignKey(nameof(PollDtoId))]
        public PollDto PollDto { get; set; }

        public virtual ICollection<CategoryNomineeDto> CategoryNomineeDtos { get; set; }
        public virtual ICollection<VoteDto> VoteDtos { get; set; }
    }
}
