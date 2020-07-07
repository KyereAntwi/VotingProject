using DTOs.Data;
using DTOs.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly DTOs.Data.DbContext _dbContext;

        public AuthServices(UserManager<IdentityUser> userManager, 
                            JwtSettings jwtSettings, 
                            TokenValidationParameters validationParameters,
                            DTOs.Data.DbContext dbContext)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = validationParameters;
            _dbContext = dbContext;
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

            return await GenerateTokenAsync(newUser);
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

            return await GenerateTokenAsync(newUser);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFronToken(token);
            if (validatedToken == null)
                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };
            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
                return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value;
            var storedRefreshToken = await _dbContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
                return new AuthenticationResult { Errors = new[] { "This refresh token does not exist" } };

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                return new AuthenticationResult { Errors = new[] { "This refresh token has expired" } };

            if (storedRefreshToken.Invalidated)
                return new AuthenticationResult { Errors = new[] { "This refresh token has been invalidated" } };

            if (storedRefreshToken.Used)
                return new AuthenticationResult { Errors = new[] { "This refresh token has been used" } };

            if (storedRefreshToken.JwtId != jti)
                return new AuthenticationResult { Errors = new[] { "This refresh token does not match this JwT" } };

            storedRefreshToken.Used = true;
            _dbContext.RefreshTokens.Update(storedRefreshToken);
            await _dbContext.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
            return await GenerateTokenAsync(user);
        }

        private ClaimsPrincipal GetPrincipalFronToken(string token) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken)) { return null; }
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken) 
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) && 
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
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
        private async Task<AuthenticationResult> GenerateTokenAsync(IdentityUser newUser)
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
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken 
            {
                JwtId = token.Id,
                UserId = newUser.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();


            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }
    }
}
