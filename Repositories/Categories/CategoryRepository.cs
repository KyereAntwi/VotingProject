using DTOs.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DTOs.Data.DbContext _dbContext;

        public CategoryRepository(DTOs.Data.DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryNomineeDto> AddNomineeToCategoryAsync(Guid CategoryId, Guid NomineeId)
        {
            var model = new CategoryNomineeDto 
            {
                CategoryDtoId = CategoryId,
                NomineeDtoId = NomineeId
            };

            await _dbContext.CategoryNomineeDtos.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto category)
        {
            await _dbContext.CategoryDtos.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesOfPoll(Guid PollId)
        {
            var categories = await _dbContext.CategoryDtos
                .Include(c => c.CategoryNomineeDtos)
                .Include(c => c.VoteDtos)
                .Where(c => c.PollDtoId == PollId)
                .ToListAsync();

            return categories;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid Id)
        {
            var category = await _dbContext.CategoryDtos
                .Include(c => c.CategoryNomineeDtos)
                .Include(c => c.VoteDtos)
                .SingleOrDefaultAsync(c => c.PollDtoId == Id);

            return category;
        }

        public async Task<CategoryDto> RemoveCategoryAsync(Guid Id)
        {
            var category = await _dbContext.CategoryDtos.FindAsync(Id);

            if (category == null)
                return null;

            _dbContext.CategoryDtos.Remove(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<CategoryNomineeDto> RemoveNomineeFromCategoryAsync(Guid CategoryId, Guid NomineeId)
        {
            var categoryNominee = await _dbContext
                .CategoryNomineeDtos
                .SingleOrDefaultAsync(n => n.CategoryDtoId == CategoryId && n.NomineeDtoId == NomineeId);

            if (categoryNominee == null)
                return null;

            _dbContext.CategoryNomineeDtos.Remove(categoryNominee);
            await _dbContext.SaveChangesAsync();
            return categoryNominee;
        }
    }
}
