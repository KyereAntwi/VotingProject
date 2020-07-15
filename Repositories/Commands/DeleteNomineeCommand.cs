using Contracts.Responses.V1;
using MediatR;
using System;

namespace Repositories.Commands
{
    public class DeleteNomineeCommand : IRequest<NomineeResponse>
    {
        public Guid Id { get; set; }
    }
}
