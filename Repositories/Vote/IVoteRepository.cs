using DTOs.DTOs;
using System.Threading.Tasks;

namespace Repositories.Vote
{
    public interface IVoteRepository
    {
        Task<VoteDto> PerformVoteAsync(VoteDto vote);
    }
}
