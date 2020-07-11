using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTOs.DTOs
{
    public class PollDto
    {
        [Key]
        public Guid Id { get; set; }
        public string Theme { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartedDateTime { get; set; }
        public DateTime EndedDateTime { get; set; }

        public virtual ICollection<CategoryDto> CategoryDtos { get; set; }
        public virtual ICollection<ElectoralCommisionerDto> ElectoralCommisionerDtos { get; set; }
    }
}
