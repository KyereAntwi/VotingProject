using System.Threading.Tasks;
using Contracts;
using Contracts.Responses.V1;
using Contracts.ViewModels.V1;
using DTOs.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Identity;

namespace WepApi.Controllers.V1
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, EC-Official")]
        [HttpPost(ApiRoutes.Auth.Register)]
        public async Task<IActionResult> RegisterAsync([FromBody]UserRegistrationRequest request) 
        {
            AuthenticationResult result = new AuthenticationResult 
            {
                Success = false
            };

            if (!string.IsNullOrEmpty(request.Password)) 
            {
                result = await _authServices.RegisterWithPasswordAsync(request.Username, request.Password, request.Role);
            }

            if (string.IsNullOrEmpty(request.Password)) 
            {
                result = await _authServices.RegisterWithoutPasswordAsync(request.Username, request.Role);
            }

            if (!result.Success) 
            {
                return BadRequest (new AuthFailedResponse
                {
                    Errors = result.Errors
                });
            }

            // if gets here then the implementation is correct
            return Ok(new AuthSuccessResponse { Token = "Secret" });
        }
    }
}
