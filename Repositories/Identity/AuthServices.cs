using DTOs.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Identity
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthServices(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<bool> DeleteAUserAsync(string username)
        {
            var existedUser = await _userManager.FindByNameAsync(username);

            if (existedUser == null) 
            {
                return false;
            }

            var deletionResponse = await _userManager.DeleteAsync(existedUser);

            if (!deletionResponse.Succeeded)
                return false;
            return true;
        }

        public async Task<AuthenticationResult> LoginWithoutPasswordAsync(string username)
        {
            var existingUser = await _userManager.FindByNameAsync(username);

            if (existingUser == null)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { "User does not exist" }
                };
            }

            var newUser = new IdentityUser
            {
                UserName = username
            };

            return GenerateToken(newUser);
        }

        public async Task<AuthenticationResult> LoginWithPasswordAsync(string username, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(username);

            if (existingUser == null)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { "User does not exist" }
                };
            }

            var newUser = new IdentityUser
            {
                UserName = username
            };

            var checkIfPasswordIsValid = await _userManager.CheckPasswordAsync(existingUser, password);

            if (!checkIfPasswordIsValid)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { "User/Password combination is wrong" }
                };
            }

            return GenerateToken(newUser);
        }

        public async Task<AuthenticationResult> RegisterWithoutPasswordAsync(string username, string role)
        {
            var existingUser = await _userManager.FindByNameAsync(username);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { "User already exist" }
                };
            }

            var newUser = new IdentityUser
            {
                UserName = username,
                Email = username + "@example.com"
            };

            var registeredUser = await _userManager.CreateAsync(newUser);

            if (!registeredUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = registeredUser.Errors.Select(x => x.Description)
                };
            }

            var createdIdentityUser = await _userManager.FindByNameAsync(username);
            var roleAddingResult = await _userManager.AddToRoleAsync(createdIdentityUser, role);

            return new AuthenticationResult
            {
                Success = true
            };
        }

        public async Task<AuthenticationResult> RegisterWithPasswordAsync(string username, string password, string role)
        {
            var existingUser = await _userManager.FindByNameAsync(username);

            if (existingUser != null) 
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = new[] { "User already exist" }
                };
            }

            var newUser = new IdentityUser 
            {
                UserName = username,
                Email = username + "@example.com"
            };

            var registeredUser = await _userManager.CreateAsync(newUser, password);

            if (!registeredUser.Succeeded) 
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Errors = registeredUser.Errors.Select(x => x.Description)
                };
            }

            var createdIdentityUser = await _userManager.FindByNameAsync(username);
            var roleAddingResult = await _userManager.AddToRoleAsync(createdIdentityUser, role);

            return new AuthenticationResult
            {
                Success = true
            };
        }

        // private method to generate a token
        private AuthenticationResult GenerateToken(IdentityUser newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("id", newUser.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
