using Contracts.Responses.V1;
using MediatR;
using System;

namespace Repositories.Commands
{
    public class RemoveNomineeFromCategoryCommand : IRequest<CategoryResponse>
    {
        public Guid CategoryId { get; set; }
        public Guid NomineeId { get; set; }
    }
}
