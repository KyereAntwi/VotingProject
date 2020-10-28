using Contracts.Responses.V1;
using MediatR;
using Repositories.Categories;
using Repositories.Querries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class GetAllCategoriesOfPollHandler : IRequestHandler<GetAllCategoriesOfPollQuery, List<CategoryResponse>>
    {
        private readonly ICategoryRepository _catRepo;

        public GetAllCategoriesOfPollHandler(ICategoryRepository categoryRepository)
        {
            _catRepo = categoryRepository;
        }

        async Task<List<CategoryResponse>> IRequestHandler<GetAllCategoriesOfPollQuery, List<CategoryResponse>>.Handle(GetAllCategoriesOfPollQuery request, CancellationToken cancellationToken)
        {
            var categoryList = await _catRepo.GetAllCategoriesOfPoll(request.PollId);
            List<CategoryResponse> responses = new List<CategoryResponse>();
            if (categoryList.Count > 0) 
            {
                foreach (var cat in categoryList) 
                {
                    var newCategory = new CategoryResponse
                    {
                        Id = cat.Id,
                        Theme = cat.Theme,
                        PollId = cat.PollDtoId
                    };

                    if (cat.CategoryNomineeDtos.Count > 0) 
                    {
                        List<NomineeResponse> categoryNominees = new List<NomineeResponse>();
                        foreach (var nominee in cat.CategoryNomineeDtos) 
                        {
                            categoryNominees.Add(new NomineeResponse
                            {
                                Id = nominee.NomineeDtoId
                            });
                        }

                        newCategory.Nominees = categoryNominees;
                    }

                    if (cat.VoteDtos.Count > 0) 
                    {
                        List<VoteResponse> categoryVotes = new List<VoteResponse>();
                        foreach (var vote in cat.VoteDtos) 
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

                    responses.Add(newCategory);
                }
            }

            return responses;
        }
    }
}
