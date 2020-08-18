using Contracts.Responses.V1;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Repositories.Querries
{
    public class GetAllCategoriesOfPollAvailableQuery : IRequest<List<CategoryResponse>>
    {
        public Guid PollId { get; }
        public IdentityUser User { get; set; }

        public GetAllCategoriesOfPollAvailableQuery(Guid pollId, IdentityUser user)
        {
            PollId = pollId;
            User = user;
        }
    }
}
