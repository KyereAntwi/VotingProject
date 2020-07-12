using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Contracts.Responses.V1;
using Contracts.ViewModels.V1;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Commands;
using Repositories.Querries;

namespace WepApi.Controllers.V1
{
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PollsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PollsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Polls.GetAllPolls)]
        public async Task<IActionResult> GetAllPollsAsync() 
        {
            var querry = new GetAllPollsQuery();
            var result = await _mediator.Send(querry);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Polls.GetSinglePoll)]
        public async Task<IActionResult> GetPollByIdAsync([FromRoute] Guid Id) 
        {
            if (Id == Guid.Empty)
            {
                return BadRequest(new RequestFailedResponse
                {
                    Errors = new[] { "No Id was provided" }
                });
            }

            var query = new GetPollByIdQuery(Id);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult) Ok(result) : NotFound();
        }

        [HttpPost(ApiRoutes.Polls.CreateNewPoll)]
        public async Task<IActionResult> AddAPollAsync([FromBody] NewPollRequest request) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(new RequestFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }


            var result = await _mediator.Send(new AddPollCammand
            {
                Theme = request.Theme,
                Description = request.Description,
                CreatedAt = DateTime.Now,
                StartedDateTime = request.StartedDateTime,
                EndedDateTime = request.EndedDateTime
            });
            return CreatedAtAction(nameof(GetPollByIdAsync), new { Id = result.Id }, result);
        }

        [HttpDelete(ApiRoutes.Polls.DeletePoll)]
        public async Task<IActionResult> DeletePollAsync([FromRoute] Guid Id) 
        {
            if (Id == Guid.Empty)
            {
                return BadRequest(new RequestFailedResponse
                {
                    Errors = new[] { "No Id was provided" }
                });
            }

            var result = await _mediator.Send(new DeletePollCommand { Id = Id });
            return result != null ? (IActionResult) Accepted(result) : NotFound();
        }
    }
}
