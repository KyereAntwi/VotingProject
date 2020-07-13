using Contracts.Responses.V1;
using MediatR;
using System;

namespace Repositories.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryResponse>
    {
        public string Theme { get; set; }
        public Guid PollDtoId { get; set; }
    }
}
