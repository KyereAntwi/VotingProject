using Contracts.Responses.V1;
using MediatR;
using Repositories.Polls;
using Repositories.Querries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class GetPollByIdHandler : IRequestHandler<GetPollByIdQuery, PollResponse>
    {
        private readonly IPollsRepository _pollRepo;

        public GetPollByIdHandler(IPollsRepository pollsRepository)
        {
            _pollRepo = pollsRepository;
        }
        public async Task<PollResponse> Handle(GetPollByIdQuery request, CancellationToken cancellationToken)
        {
            var poll = await _pollRepo.GetSinglePollResponseAsync(request.Id);
            
            PollResponse pollResponse = new PollResponse();
            if (poll != null)
            {
                pollResponse.Id = poll.Id;
                pollResponse.Theme = poll.Theme;
                pollResponse.Description = poll.Description;
                pollResponse.CreatedAt = poll.CreatedAt;
                poll.StartedDateTime = poll.StartedDateTime;
                poll.EndedDateTime = poll.EndedDateTime;

                if (poll.CategoryDtos.Count > 0)
                {
                    var catList = new List<CategoryResponse>();
                    foreach (var cat in poll.CategoryDtos)
                    {
                        catList.Add(new CategoryResponse { Id = cat.Id });
                    }

                    pollResponse.Categories = catList;
                }

                if (poll.ElectoralCommisionerDtos.Count > 0)
                {
                    var eComList = new List<ApplicationUserResponse>();
                    foreach (var e in poll.ElectoralCommisionerDtos)
                    {
                        eComList.Add(new ApplicationUserResponse { Username = e.User.UserName });
                    }

                    pollResponse.ElectoralCommissioners = eComList;
                }

                return pollResponse;
            }
            else 
            {
                return null;
            }
        }
    }
}
