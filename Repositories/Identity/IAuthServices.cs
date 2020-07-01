using DTOs.Domain;
using System.Threading.Tasks;

namespace Repositories.Identity
{
    public interface IAuthServices
    {
        Task<AuthenticationResult> RegisterWithPasswordAsync(string username, string password, string role);
        Task<AuthenticationResult> RegisterWithoutPasswordAsync(string username, string role);
        Task<AuthenticationResult> LoginWithPasswordAsync(string username, string password);
        Task<AuthenticationResult> LoginWithoutPasswordAsync(string username);
        Task<bool> DeleteAUserAsync(string username);
    }
}
