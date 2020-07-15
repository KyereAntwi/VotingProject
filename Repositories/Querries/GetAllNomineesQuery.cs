using Contracts.Responses.V1;
using MediatR;
using System.Collections.Generic;

namespace Repositories.Querries
{
    public class GetAllNomineesQuery : IRequest<List<NomineeResponse>>
    {
    }
}
