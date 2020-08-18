using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Vote
{
    public interface IVoteRepository
    {
        Task<VoteDto> PerformVoteAsync(VoteDto vote);
        Task<List<VoteDto>> GetAllVotesOfCategoryAsync(Guid CategoryId);
    }
}
