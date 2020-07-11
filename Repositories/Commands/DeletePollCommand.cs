using Contracts.Responses.V1;
using MediatR;
using System;

namespace Repositories.Commands
{
    public class DeletePollCommand : IRequest<PollResponse>
    {
        public Guid Id { get; set; }
    }
}
