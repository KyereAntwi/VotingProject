using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Identity;

namespace WepApi.Installers
{
    public class RepositoriesInstallers : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IAuthServices, AuthServices>();
        }
    }
}
