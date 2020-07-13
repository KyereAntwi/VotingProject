using Contracts.Responses.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using Repositories.Categories;
using Repositories.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class RemoveCategoryHandler : IRequestHandler<RemoveCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _cateRepo;
        private readonly ILogger<RemoveCategoryHandler> _logger;

        public RemoveCategoryHandler(ICategoryRepository categoryRepository, ILogger<RemoveCategoryHandler> logger)
        {
            _cateRepo = categoryRepository;
            _logger = logger;
        }

        public async Task<CategoryResponse> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            var removedCate = await _cateRepo.RemoveCategoryAsync(request.Id);

            if (removedCate == null)
                return null;

            _logger.LogInformation($"Removed a Category with Id of: {removedCate.Id}");

            return new CategoryResponse 
            {
                Id = removedCate.Id,
                Theme = removedCate.Theme,
                PollId = removedCate.PollDtoId
            };
        }
    }
}
