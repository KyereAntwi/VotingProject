using DTOs.DTOs;
using System.Collections.Generic;

namespace WepApi.Models
{
    public class CategoryResultViewModel
    {
        public List<NomineeVotes> Votes { get; set; }
        public CategoryDto Catogory { get; set; }
    }
}
