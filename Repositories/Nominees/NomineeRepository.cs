using DTOs.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Nominees
{
    public class NomineeRepository : INomineeRepository
    {
        private readonly DTOs.Data.DbContext _dbContext;

        public NomineeRepository(DTOs.Data.DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<NomineeDto> CreateANomineeAsync(NomineeDto nomineeDto)
        {
            await _dbContext.AddAsync(nomineeDto);
            await _dbContext.SaveChangesAsync();
            return nomineeDto;
        }

        public async Task<NomineeDto> DeleteANomineeAsync(Guid Id)
        {
            var nominee = await _dbContext.NomineeDtos.FindAsync(Id);
            
            if (nominee == null)
                return null;

            _dbContext.NomineeDtos.Remove(nominee);
            await _dbContext.SaveChangesAsync();
            return nominee;
        }

        public async Task<List<NomineeDto>> GetAllNomineesAsync()
        {
            return await _dbContext.NomineeDtos.Include(n => n.CategoryNomineeDtos).ToListAsync();
        }

        public async Task<NomineeDto> GetASingleNomineeAsync(Guid Id)
        {
            return await _dbContext.NomineeDtos.Include(n => n.CategoryNomineeDtos).SingleOrDefaultAsync(n => n.Id == Id);
        }

        public async Task<int> GetTotalVotesForNomineePerCategoryAsync(Guid NomineeId, Guid CategoryId)
        {
            var votes = await _dbContext.VoteDtos.Where(v => v.NomineeDtoId == NomineeId && v.CategoryDtoId == CategoryId).ToListAsync();
            return votes.Count;
        }
    }
}
