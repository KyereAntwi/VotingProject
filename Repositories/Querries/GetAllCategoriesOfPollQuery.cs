using Contracts.Responses.V1;
using MediatR;
using System;
using System.Collections.Generic;

namespace Repositories.Querries
{
    public class GetAllCategoriesOfPollQuery : IRequest<List<CategoryResponse>>
    {
        public Guid PollId { get; }

        public GetAllCategoriesOfPollQuery(Guid pollId)
        {
            PollId = pollId;
        }
    }
}
