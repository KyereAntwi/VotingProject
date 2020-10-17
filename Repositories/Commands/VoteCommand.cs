using Contracts.Responses.V1;
using MediatR;
using System;

namespace Repositories.Commands
{
    public class VoteCommand : IRequest<VoteResponse>
    {
        public Guid NomineeId { get; set; }
        public Guid CategoryId { get; set; }
        public string Username { get; set; }
    }
}
