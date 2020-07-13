using System;
using System.Collections.Generic;

namespace Contracts.Responses.V1
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Theme { get; set; }
        public Guid PollId { get; set; }
        public IEnumerable<NomineeResponse> Nominees { get; set; }
        public IEnumerable<VoteResponse> Votes { get; set; }
    }
}
