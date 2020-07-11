using Contracts.Responses.V1;
using MediatR;
using System;

namespace Repositories.Commands
{
    public class AddPollCammand : IRequest<PollResponse>
    {
        public string Theme { get; set; }
        public string Description { get; set; }
        public DateTime StartedDateTime { get; set; }
        public DateTime EndedDateTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
