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
using WepApi.Helpers;

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
            return Ok(new AuthSuccessResponse { Token = result.Token, RefreshToken = result.RefreshToken  });
        }

        [HttpPost(ApiRoutes.Auth.Refresh)]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request) 
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

            var authResponse = await _authServices.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!result.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = result.Errors
                });
            }

            // if gets here then the implementation is correct
            return Ok(new AuthSuccessResponse { Token = result.Token, RefreshToken = result.RefreshToken });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        [HttpDelete(ApiRoutes.Auth.DeleteUser)]
        public async Task<IActionResult> DeleteAUserAsync([FromRoute] string Username) 
        {
            if (string.IsNullOrEmpty(Username)) 
            {
                return BadRequest(new AuthFailedResponse { Errors = new[] { "No username was provided" } });
            }

            bool result = await _authServices.DeleteAUserAsync(Username);

            if (!result) 
            {
                return BadRequest(new AuthFailedResponse { Errors = new[] { "Sorry operation failed" } });
            }

            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "EC-Official")]
        [HttpGet(ApiRoutes.VotersRegister.GenerateVoterCode)]
        public async Task<IActionResult> GenerateUsernameAsync() 
        {
            GenerateRandom generateRandom = new GenerateRandom();
            string newUsername = generateRandom.RandomChar();

            var result = await _authServices.IsUsernameExisting(newUsername);

            while (result == true)
            {
                newUsername = generateRandom.RandomChar();
                result = await _authServices.IsUsernameExisting(newUsername);
            }

            return Ok(new { Username = newUsername });
        }
    }
}
