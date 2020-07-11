using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTOs.DTOs
{
    public class CategoryNomineeDto
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CategoryDtoId { get; set; }
        [ForeignKey(nameof(CategoryDtoId))]
        public CategoryDto CategoryDto { get; set; }

        public Guid NomineeDtoId { get; set; }
        [ForeignKey(nameof(NomineeDtoId))]
        public NomineeDto NomineeDto { get; set; }
    }
}
