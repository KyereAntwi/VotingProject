using Contracts.Responses.V1;
using MediatR;
using Repositories.Categories;
using Repositories.Querries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
    {
        private readonly ICategoryRepository _cateRepo;

        public GetCategoryByIdHandler(ICategoryRepository categoryRepository)
        {
            _cateRepo = categoryRepository;
        }
        public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _cateRepo.GetCategoryByIdAsync(request.Id);

            if (category == null)
                return null;

            var response = new CategoryResponse
            {
                Id = category.Id,
                Theme = category.Theme,
                PollId = category.PollDtoId,
            };

            if (category.CategoryNomineeDtos.Count > 0) 
            {
                List<NomineeResponse> categoryNominees = new List<NomineeResponse>();
                foreach (var nominee in category.CategoryNomineeDtos)
                {
                    categoryNominees.Add(new NomineeResponse
                    {
                        Id = nominee.NomineeDtoId,
                        Fullname = nominee.NomineeDto.Fullname,
                        ImageUrl = nominee.NomineeDto.ImageUrl
                    });
                }

                response.Nominees = categoryNominees;
            }

            if (category.VoteDtos.Count > 0)
            {
                List<VoteResponse> categoryVotes = new List<VoteResponse>();
                foreach (var vote in category.VoteDtos)
                {
                    categoryVotes.Add(new VoteResponse
                    {
                        Id = vote.Id,
                        UserId = vote.UserId,
                        CategoryDtoId = vote.CategoryDtoId,
                        CreatedAt = vote.CreatedAt,
                        NomineeDtoId = vote.NomineeDtoId
                    });
                }
            }

            return response;
        }
    }
}
