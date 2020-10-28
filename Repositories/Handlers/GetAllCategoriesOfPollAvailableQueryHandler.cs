using Contracts.Responses.V1;
using MediatR;
using Repositories.Categories;
using Repositories.Nominees;
using Repositories.Querries;
using Repositories.Vote;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class GetAllCategoriesOfPollAvailableQueryHandler : IRequestHandler<GetAllCategoriesOfPollAvailableQuery, List<CategoryResponse>>
    {
        private readonly ICategoryRepository _catRepo;
        private readonly IVoteRepository _voteRepo;
        private readonly INomineeRepository _nomineeRepository;

        public GetAllCategoriesOfPollAvailableQueryHandler(ICategoryRepository categoryRepository, 
                                                           IVoteRepository voteRepository,
                                                           INomineeRepository nomineeRepo)
        {
            _catRepo = categoryRepository;
            _voteRepo = voteRepository;
            _nomineeRepository = nomineeRepo;

        }
        public async Task<List<CategoryResponse>> Handle(GetAllCategoriesOfPollAvailableQuery request, CancellationToken cancellationToken)
        {
            var categoryList = await _catRepo.GetAllCategoriesOfPoll(request.PollId);
            List<CategoryResponse> responses = new List<CategoryResponse>();

            if (categoryList.Count > 0)
            {
                foreach (var cat in categoryList)
                {
                    var voteList = await _voteRepo.GetAllVotesOfCategoryAsync(cat.Id);
                    bool available = true;

                    if (voteList.Count > 0) 
                    {
                        foreach (var vote in voteList)
                        {
                            if (vote.User == request.User)
                                available = false;
                        }
                    }

                    if (available)
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
                                var nomineeFound = await _nomineeRepository.GetASingleNomineeAsync(nominee.NomineeDtoId);
                                categoryNominees.Add(new NomineeResponse
                                {
                                    Id = nominee.NomineeDtoId,
                                    Fullname = nomineeFound.Fullname,
                                    ImageUrl = nomineeFound.ImageUrl != null ? nomineeFound.ImageUrl : ""
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
            }

            return responses;
        }
    }
}
