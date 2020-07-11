using Contracts.Responses.V1;
using MediatR;
using System;

namespace Repositories.Querries
{
    public class GetPollByIdQuery : IRequest<PollResponse>
    {
        public Guid Id { get; }

        public GetPollByIdQuery(Guid Id)
        {
            this.Id = Id;
        }
    }
}
