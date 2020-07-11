using DTOs.Domain;
using DTOs.DTOs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DTOs.Data
{
    public class DbContext: IdentityDbContext
    {
        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ActivityDto> ActivityDtos { get; set; }
        public DbSet<CategoryDto> CategoryDtos { get; set; }
        public DbSet<CategoryNomineeDto> CategoryNomineeDtos { get; set; }
        public DbSet<ElectoralCommisionerDto> ElectoralCommisionerDtos { get; set; }
        public DbSet<NomineeDto> NomineeDtos { get; set; }
        public DbSet<PollDto> PollDtos { get; set; }
        public DbSet<VoteDto> VoteDtos { get; set; }
    }
}
