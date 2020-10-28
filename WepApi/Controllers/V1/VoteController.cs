using System.Threading.Tasks;
using Contracts;
using Contracts.ViewModels.V1;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Commands;

namespace WepApi.Controllers.V1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Voter")]
    public class VoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(ApiRoutes.VotersRegister.Vote)]
        public async Task<IActionResult> PerformVoteAsync([FromBody]VoteRequest request) 
        {
            var result = await _mediator.Send(new VoteCommand
            {
                CategoryId = request.CategoryId,
                NomineeId = request.NomineeId,
                Username = request.Username
            });

            return result != null ? (IActionResult)Accepted(result) : BadRequest(
                new { Error = "Category or Nominee specified does not exist or you have already voted for this category"}
                );
        }
    }
}
