using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTOs.DTOs
{
    public class NomineeDto
    {
        [Key]
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public Uri ImageUrl { get; set; }
        public virtual ICollection<CategoryNomineeDto> CategoryNomineeDtos { get; set; }
    }
}
