using Contracts.Responses.V1;
using MediatR;
using Repositories.Polls;
using Repositories.Querries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class GetAllPollsHandler : IRequestHandler<GetAllPollsQuery, List<PollResponse>>
    {
        private readonly IPollsRepository _pollsRepo;

        public GetAllPollsHandler(IPollsRepository pollsRepository)
        {
            _pollsRepo = pollsRepository;
        }
        public async Task<List<PollResponse>> Handle(GetAllPollsQuery request, CancellationToken cancellationToken)
        {
            var pollList = await _pollsRepo.GetAllPollResponsesAsync();
            List<PollResponse> responses = new List<PollResponse>();
            
            if (pollList.Count > 0) 
            {
                foreach (var poll in pollList)
                {
                    var newPoll = new PollResponse
                    {
                        Id = poll.Id,
                        Theme = poll.Theme,
                        Description = poll.Description,
                        StartedDateTime = poll.StartedDateTime,
                        EndedDateTime = poll.EndedDateTime
                    };

                    if (poll.CategoryDtos.Count > 0)
                    {
                        var catList = new List<CategoryResponse>();
                        foreach (var cat in poll.CategoryDtos)
                        {
                            catList.Add(new CategoryResponse { Id = cat.Id });
                        }

                        newPoll.Categories = catList;
                    }

                    if (poll.ElectoralCommisionerDtos.Count > 0) 
                    {
                        var eComList = new List<ApplicationUserResponse>();
                        foreach (var e in poll.ElectoralCommisionerDtos) 
                        {
                            eComList.Add(new ApplicationUserResponse { Username = e.User.UserName });
                        }

                        newPoll.ElectoralCommissioners = eComList;
                    }

                    responses.Add(newPoll);
                }
            }

            return responses;
        }
    }
}
