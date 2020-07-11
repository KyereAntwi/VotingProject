using Contracts.Responses.V1;
using DTOs.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;
using Repositories.Commands;
using Repositories.Polls;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class AddPollHandler : IRequestHandler<AddPollCammand, PollResponse>
    {
        private readonly IPollsRepository _pollRepo;
        private readonly ILogger<AddPollHandler> _logger;

        public AddPollHandler(IPollsRepository pollsRepository, ILogger<AddPollHandler> logger)
        {
            _pollRepo = pollsRepository;
            _logger = logger;
        }

        public async Task<PollResponse> Handle(AddPollCammand request, CancellationToken cancellationToken)
        {

            var newPoll = await _pollRepo.AddPollAsync(new PollDto
            {
                Theme = request.Theme,
                Description = request.Description,
                CreatedAt = request.CreatedAt,
                StartedDateTime = request.StartedDateTime,
                EndedDateTime = request.EndedDateTime
            });
            _logger.LogInformation($"Added a new Poll with Id of: {newPoll.Id}");

            return new PollResponse 
            {
                Id = newPoll.Id,
                Theme = newPoll.Theme,
                Description = newPoll.Description,
                CreatedAt = newPoll.CreatedAt,
                StartedDateTime = newPoll.StartedDateTime,
                EndedDateTime = newPoll.EndedDateTime
            };
        }
    }
}
