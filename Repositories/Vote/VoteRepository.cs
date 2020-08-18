using DTOs.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Vote
{
    public class VoteRepository : IVoteRepository
    {
        private readonly DTOs.Data.DbContext _db;

        public VoteRepository(DTOs.Data.DbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<List<VoteDto>> GetAllVotesOfCategoryAsync(Guid CategoryId)
        {
            return await _db.VoteDtos
                .Include(vote => vote.User)
                .Where(vote => vote.CategoryDtoId == CategoryId)
                .ToListAsync();
        }

        public async Task<VoteDto> PerformVoteAsync(VoteDto vote)
        {
            var category = await _db.CategoryDtos.FindAsync(vote.CategoryDtoId);
            var nominee = await _db.NomineeDtos.FindAsync(vote.NomineeDtoId);

            if (category == null || nominee == null)
                return null;

            await _db.VoteDtos.AddAsync(vote);
            await _db.SaveChangesAsync();
            return vote;
        }
    }
}
