using Contracts.Responses.V1;
using DTOs.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;
using Repositories.Categories;
using Repositories.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _cateRepo;
        private readonly ILogger<CreateCategoryHandler> _logger;

        public CreateCategoryHandler(ICategoryRepository categoryRepository, ILogger<CreateCategoryHandler> logger)
        {
            _cateRepo = categoryRepository;
            _logger = logger;
        }

        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new CategoryDto
            {
                Theme = request.Theme,
                PollDtoId = request.PollDtoId
            };

            var response = await _cateRepo.CreateCategoryAsync(newCategory);
            _logger.LogInformation($"Added a new Category with Id of: {response.Id} to a Poll of Id of: {response.PollDtoId}");


            return new CategoryResponse
            {
                Theme = response.Theme,
                PollId = response.PollDtoId
            };
        }
    }
}
