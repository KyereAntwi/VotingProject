using Contracts.Responses.V1;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Querries
{
    public class GetAllPollsQuery : IRequest<List<PollResponse>>
    {
    }
}
