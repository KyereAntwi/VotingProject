﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Contracts.Responses.V1;
using Contracts.ViewModels.V1;
using DTOs.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Identity;
using WepApi.Helpers;

namespace WepApi.Controllers.V1
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(IAuthServices authServices, UserManager<IdentityUser> userManager)
        {
            _authServices = authServices;
            _userManager = userManager;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator, Ecoffical")]
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
            return Ok(new AuthSuccessResponse { Token = result.Token, RefreshToken = result.RefreshToken });
        }

        [HttpGet(ApiRoutes.Auth.PerformMasterRegistration)]
        public async Task<IActionResult> MasterRegistrationAsync()
        {
            UserRegistrationRequest request = new UserRegistrationRequest 
            {
                Username = "MasterUser",
                Password = "MasterPassword",
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
            return Ok(new AuthSuccessResponse { Token = result.Token, RefreshToken = result.RefreshToken });
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

            var roles = await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(request.Username));

            var response = new AuthSuccessResponse
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken,
                User = new ApplicationUserResponse 
                {
                    Username = request.Username,
                    Role = roles[0]
                }
            };

            // if gets here then the implementation is correct
            return Ok(response);
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(ApiRoutes.Auth.GetUser)]
        public async Task<IActionResult> GetUserInfoAsync([FromRoute] string Username) 
        {
            var result = await _userManager.FindByNameAsync(Username);
            var roles = await _userManager.GetRolesAsync(result);
            var response = new AuthSuccessResponse
            {
                User = new ApplicationUserResponse
                {
                    Username = result.UserName,
                    Role = roles[0]
                }
            };
            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Ecoffical, Administrator")]
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        [HttpGet(ApiRoutes.Auth.GetAllManagers)]
        public async Task<IActionResult> GetAllSystemManagers() 
        {
            List<ApplicationUserResponse> response = new List<ApplicationUserResponse>();

            var adminUsers = await _userManager.GetUsersInRoleAsync("Administrator");
            var ecUsers = await _userManager.GetUsersInRoleAsync("Ecoffical");

            if (adminUsers.Count > 0) 
            {
                foreach (var user in adminUsers) 
                {
                    response.Add(new ApplicationUserResponse
                    {
                        Username = user.UserName,
                        Role = "Administrator"
                    });
                }
            }

            if (ecUsers.Count > 0)
            {
                foreach (var user in ecUsers)
                {
                    response.Add(new ApplicationUserResponse
                    {
                        Username = user.UserName,
                        Role = "Ecoffical"
                    });
                }
            }

            return Ok(response);
        }
    }
}
