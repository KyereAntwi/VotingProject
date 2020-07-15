using Contracts.Responses.V1;
using MediatR;
using System;

namespace Repositories.Querries
{
    public class GetASingleNomineeQuery : IRequest<NomineeResponse>
    {
        public Guid Id { get; set; }
        public GetASingleNomineeQuery(Guid id)
        {
            Id = id;
        }
    }
}
