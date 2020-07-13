using System;

namespace Contracts.ViewModels.V1
{
    public class CreateCategoryRequest
    {
        public string Theme { get; set; }
        public Guid PollId { get; set; }
    }
}
