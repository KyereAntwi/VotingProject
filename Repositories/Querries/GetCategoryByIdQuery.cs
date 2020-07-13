using Contracts.Responses.V1;
using MediatR;
using System;

namespace Repositories.Querries
{
    public class GetCategoryByIdQuery : IRequest<CategoryResponse>
    {
        public Guid Id { get; set; }
        public GetCategoryByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
