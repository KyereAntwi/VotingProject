using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Categories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetAllCategoriesOfPoll(Guid PollId);
        Task<CategoryDto> GetCategoryByIdAsync(Guid Id);
        Task<CategoryDto> CreateCategoryAsync(CategoryDto category);
        Task<CategoryDto> RemoveCategoryAsync(Guid Id);
        Task<CategoryNomineeDto> AddNomineeToCategoryAsync(Guid CategoryId, Guid NomineeId);
        Task<CategoryNomineeDto> RemoveNomineeFromCategoryAsync(Guid CategoryId, Guid NomineeId);
    }
}
