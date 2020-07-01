using System.Linq;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

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

        [HttpPost(ApiRoutes.Auth.PerformMasterRegistration)]
        public async Task<IActionResult> MasterRegistrationAsync()
        {
            UserRegistrationRequest request = new UserRegistrationRequest 
            {
                Username = "MasterUser",
                Password = "MasterPasswordForVotes",
                Role = "Administrator"
            };

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
                return BadRequest(new AuthFailedResponse
                {
                    Errors = result.Errors
                });
            }

            // if gets here then the implementation is correct
            return Ok(new AuthSuccessResponse { Token = "Secret" });
        }

        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            AuthenticationResult result = new AuthenticationResult
            {
                Success = false
            };

            if (!string.IsNullOrEmpty(request.Password))
            {
                result = await _authServices.LoginWithPasswordAsync(request.Username, request.Password);
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                result = await _authServices.LoginWithoutPasswordAsync(request.Username);
            }

            if (!result.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = result.Errors
                });
            }

            // if gets here then the implementation is correct
            return Ok(new AuthSuccessResponse { Token = result.Token });
        }
    }
}
