using Contracts.Responses.V1;
using DTOs.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;
using Repositories.Commands;
using Repositories.Vote;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class VoteHandler : IRequestHandler<VoteCommand, VoteResponse>
    {
        private readonly ILogger<VoteHandler> _logger;
        private readonly IVoteRepository _voteRepo;

        public VoteHandler(ILogger<VoteHandler> logger, IVoteRepository voteRepository)
        {
            _logger = logger;
            _voteRepo = voteRepository;
        }
        public async Task<VoteResponse> Handle(VoteCommand request, CancellationToken cancellationToken)
        {
            var result = await _voteRepo.PerformVoteAsync(new VoteDto
            {
                UserId = request.UserId,
                CategoryDtoId = request.CategoryId,
                NomineeDtoId = request.NomineeId,
                CreatedAt = DateTime.Now
            });

            if (result == null)
                return null;

            _logger
                .LogInformation($"A vote is performed by User with Id: {result.UserId} for a Nominee with Id: {result.NomineeDtoId} in Category with Id: {result.CategoryDtoId}");
            
            return new VoteResponse 
            {
                Id = result.Id,
                CategoryDtoId = result.CategoryDtoId,
                CreatedAt = result.CreatedAt,
                NomineeDtoId = result.NomineeDtoId,
                UserId = result.UserId
            };
        }
    }
}
