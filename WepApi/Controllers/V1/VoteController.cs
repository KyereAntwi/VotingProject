using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Contracts.ViewModels.V1;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repositories.Commands;

namespace WepApi.Controllers.V1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VoteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;

        public VoteController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpPost(ApiRoutes.VotersRegister.Vote)]
        public async Task<IActionResult> PerformVoteAsync([FromBody]VoteRequest request) 
        {
            var result = await _mediator.Send(new VoteCommand
            {
                CategoryId = request.CategoryId,
                NomineeId = request.NomineeId,
                UserId = _userManager.GetUserAsync(User).Result.Id
            });

            return result != null ? (IActionResult)Accepted(result) : NotFound();
        }
    }
}
