using System;
using System.Threading.Tasks;
using Contracts;
using Contracts.ViewModels.V1;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repositories.Commands;
using Repositories.Querries;

namespace WepApi.Controllers.V1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;

        public CategoriesController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet(ApiRoutes.Categories.GetAllCategoriesOfPoll)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> GetAllCategoriesOfAPollAsync([FromRoute] Guid PollId)
        {
            var querry = new GetAllCategoriesOfPollQuery(PollId);
            var result = await _mediator.Send(querry);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Categories.GetAllCategoriesOfPollAvailable)]
        public async Task<IActionResult> GetAllCategoriesOfAPollAvailableAsync([FromRoute] Guid PollId, [FromRoute] string Username)
        {
            var user = await _userManager.FindByNameAsync(Username);
            var querry = new GetAllCategoriesOfPollAvailableQuery(PollId, user);
            var result = await _mediator.Send(querry);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Categories.GetSingleCategoryOfPoll)]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] Guid Id)
        {
            var querry = new GetCategoryByIdQuery(Id);
            var result = await _mediator.Send(querry);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPost(ApiRoutes.Categories.CreateACategoryForPoll)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> CreateCategoryAsync([FromRoute] Guid PollId, [FromBody] CreateCategoryRequest request)
        {
            var result = await _mediator.Send(new CreateCategoryCommand
            {
                Theme = request.Theme,
                PollDtoId = request.PollId
            });
            return CreatedAtAction(nameof(GetCategoryByIdAsync), new { Id = result.Id }, result);
        }

        [HttpDelete(ApiRoutes.Categories.RemoveACategoryOfPoll)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid Id)
        {
            var result = await _mediator.Send(new RemoveCategoryCommand
            {
                Id = Id
            });
            return result != null ? (IActionResult)Accepted(result) : NotFound();
        }

        [HttpPost(ApiRoutes.Categories.AddNomineeToCategory)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> AddNomineeToCategoryAsync([FromRoute] Guid CategoryId, [FromRoute] Guid NomineeId)
        {
            var result = await _mediator.Send(new AddNomineeToCategoryCommand
            {
                CategoryId = CategoryId,
                NomineeId = NomineeId
            });
            return result != null ? (IActionResult)Ok() : NotFound();
        }

        [HttpDelete(ApiRoutes.Categories.RemoveNomineeFromCategory)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> RemoveNomineeFromCategoryAsync([FromRoute] Guid CategoryId, [FromRoute] Guid NomineeId)
        {
            var result = await _mediator.Send(new RemoveNomineeFromCategoryCommand
            {
                CategoryId = CategoryId,
                NomineeId = NomineeId
            });

            return result != null ? (IActionResult)Ok() : NotFound();
        }
    }
}
