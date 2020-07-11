using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Polls
{
    public interface IPollsRepository
    {
        Task<List<PollDto>> GetAllPollResponsesAsync();
        Task<PollDto> GetSinglePollResponseAsync(Guid Id);
        Task<PollDto> AddPollAsync(PollDto poll);
        Task<PollDto> DeletePollAsync(Guid Id);
    }
}
