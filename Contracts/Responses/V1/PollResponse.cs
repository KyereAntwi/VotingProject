using System;
using System.Collections.Generic;

namespace Contracts.Responses.V1
{
    public class PollResponse
    {
        public Guid Id { get; set; }
        public string Theme { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartedDateTime { get; set; }
        public DateTime EndedDateTime { get; set; }

        public IEnumerable<CategoryResponse> Categories { get; set; }
        public IEnumerable<ApplicationUserResponse> ElectoralCommissioners { get; set; }
    }
}
