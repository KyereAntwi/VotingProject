using Contracts.Responses.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using Repositories.Categories;
using Repositories.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class AddNomineeToCategoryHandler : IRequestHandler<AddNomineeToCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _cateRepo;
        private readonly ILogger<AddNomineeToCategoryHandler> _logger;

        public AddNomineeToCategoryHandler(ICategoryRepository categoryRepository, ILogger<AddNomineeToCategoryHandler> logger)
        {
            _cateRepo = categoryRepository;
            _logger = logger;
        }

        public async Task<CategoryResponse> Handle(AddNomineeToCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _cateRepo.AddNomineeToCategoryAsync(request.CategoryId, request.NomineeId);

            if (category == null)
                return null;

            _logger.LogInformation($"A Nominee of Id: {category.NomineeDtoId} was added to Category of Id: {category.CategoryDtoId}");

            return new CategoryResponse 
            {
                Id = category.CategoryDtoId
            };
        }
    }
}
