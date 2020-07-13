using Contracts.Responses.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using Repositories.Categories;
using Repositories.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class RemoveNomineeFromCategoryHandler : IRequestHandler<RemoveNomineeFromCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _cateRepo;
        private readonly ILogger<RemoveNomineeFromCategoryHandler> _logger;

        public RemoveNomineeFromCategoryHandler(ICategoryRepository categoryRepository, ILogger<RemoveNomineeFromCategoryHandler> logger)
        {
            _cateRepo = categoryRepository;
            _logger = logger;
        }
        public async Task<CategoryResponse> Handle(RemoveNomineeFromCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = await _cateRepo.RemoveNomineeFromCategoryAsync(request.CategoryId, request.NomineeId);

            if (response == null)
                return null;

            _logger.LogInformation($"A Nominee of Id: {response.NomineeDtoId} has been removed from a Category of Id: {response.CategoryDtoId}");
            return new CategoryResponse
            {
                Id = response.CategoryDtoId
            };
        }
    }
}
