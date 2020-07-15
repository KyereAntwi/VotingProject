using System;
using System.Threading.Tasks;
using Contracts;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NomineesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NomineesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Nominees.GetAllNominees)]
        public async Task<IActionResult> GetAllNomineesAsync() 
        {
            var query = new GetAllNomineesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Nominees.GetASingleNominee)]
        public async Task<IActionResult> GetANomineeAsync([FromRoute] Guid Id) 
        {
            var query = new GetASingleNomineeQuery(Id);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPost(ApiRoutes.Nominees.AddNewNominee)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> CreateANomineeAsync([FromForm] CreateNomineeRequest request) 
        {
            var result = await _mediator.Send(new CreateANomineeCommand 
            {
                Fullname = request.Fullname,
                Avatar = request.Avatar
            });
            return CreatedAtAction(nameof(GetANomineeAsync), new { Id = result.Id }, result);
        }

        [HttpDelete(ApiRoutes.Nominees.DeleteNominee)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteNomineeAsync([FromRoute] Guid Id) 
        {
            var result = await _mediator.Send(new DeleteNomineeCommand { Id = Id });
            return result != null ? (IActionResult)Accepted(result) : NotFound();
        }
    }
}
