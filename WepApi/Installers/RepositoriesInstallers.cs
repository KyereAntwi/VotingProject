using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Identity;
using Repositories.Polls;

namespace WepApi.Installers
{
    public class RepositoriesInstallers : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IPollsRepository, PollsRepository>();
        }
    }
}
