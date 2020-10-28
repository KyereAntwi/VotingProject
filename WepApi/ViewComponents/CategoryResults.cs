using DTOs.DTOs;
using Microsoft.AspNetCore.Mvc;
using Repositories.Categories;
using Repositories.Nominees;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WepApi.Models;

namespace WepApi.ViewComponents
{
    public class CategoryResults : ViewComponent
    {
        private readonly ICategoryRepository _cateRepo;
        private readonly INomineeRepository _nomRepo;

        public CategoryResults(ICategoryRepository categoryRepository, INomineeRepository nomineeRepository)
        {
            _cateRepo = categoryRepository;
            _nomRepo = nomineeRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid CategoryId) 
        {
            var viewModel = new CategoryResultViewModel();
            viewModel.Catogory = new CategoryDto();
            viewModel.Votes = new List<NomineeVotes>();

            var category = await _cateRepo.GetCategoryByIdAsync(CategoryId);

            if (category != null)
            {
                viewModel.Catogory = category;

                foreach (var nominee in category.CategoryNomineeDtos) 
                {
                    viewModel.Votes.Add(new NomineeVotes 
                    {
                        Nominee = await _nomRepo.GetASingleNomineeAsync(nominee.NomineeDtoId),
                        TotalVote = await _nomRepo.GetTotalVotesForNomineePerCategoryAsync(nominee.NomineeDtoId, nominee.CategoryDtoId)
                    });
                }
            }
            
            return View(viewModel);
        }
    }
}
