using Contracts.Responses.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using Repositories.Commands;
using Repositories.Polls;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class DeletePollHandler : IRequestHandler<DeletePollCommand, PollResponse>
    {
        private readonly IPollsRepository _pollRepo;
        private readonly ILogger<DeletePollHandler> _logger;
        public DeletePollHandler(IPollsRepository pollsRepository, ILogger<DeletePollHandler> logger)
        {
            _pollRepo = pollsRepository;
            _logger = logger;
        }

        public async Task<PollResponse> Handle(DeletePollCommand request, CancellationToken cancellationToken)
        {
            var deletedPoll = await _pollRepo.DeletePollAsync(request.Id);

            if (deletedPoll != null)
            {
                return new PollResponse
                {
                    Id = deletedPoll.Id,
                    Theme = deletedPoll.Theme,
                    Description = deletedPoll.Description,
                    CreatedAt = deletedPoll.CreatedAt,
                    StartedDateTime = deletedPoll.StartedDateTime,
                    EndedDateTime = deletedPoll.EndedDateTime
                };
            }
            else 
            {
                return null;
            }
        }
    }
}
