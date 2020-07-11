using DTOs.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Polls
{
    public class PollsRepository : IPollsRepository
    {
        private readonly DTOs.Data.DbContext _dbContext;

        public PollsRepository(DTOs.Data.DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PollDto> AddPollAsync(PollDto poll)
        {
            await _dbContext.PollDtos.AddAsync(poll);
            await _dbContext.SaveChangesAsync();
            return poll;
        }

        public async Task<PollDto> DeletePollAsync(Guid Id)
        {
            var poll = await _dbContext.PollDtos.FindAsync(Id);
            if (poll == null)
                return null;
            _ = _dbContext.PollDtos.Remove(poll);
            await _dbContext.SaveChangesAsync();
            return poll;
        }

        public async Task<List<PollDto>> GetAllPollResponsesAsync()
        {
            var pollsList = await _dbContext.PollDtos
                .Include(p => p.ElectoralCommisionerDtos).ThenInclude(e => e.User)
                .Include(p => p.CategoryDtos)
                .ToListAsync();

            return pollsList;
        }

        public async Task<PollDto> GetSinglePollResponseAsync(Guid Id)
        {
            var poll = await _dbContext.PollDtos
                .Include(p => p.ElectoralCommisionerDtos).ThenInclude(e => e.User)
                .Include(p => p.CategoryDtos)
                .SingleOrDefaultAsync(p => p.Id == Id);

            return poll;
        }
    }
}
