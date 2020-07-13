using Contracts.Responses.V1;
using MediatR;
using System;

namespace Repositories.Commands
{
    public class RemoveCategoryCommand : IRequest<CategoryResponse>
    {
        public Guid Id { get; set; }
    }
}
