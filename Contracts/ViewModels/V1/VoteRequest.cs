using System;

namespace Contracts.ViewModels.V1
{
    public class VoteRequest
    {
        public Guid NomineeId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
