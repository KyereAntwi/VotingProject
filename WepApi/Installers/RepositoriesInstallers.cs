using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Categories;
using Repositories.Identity;
using Repositories.Nominees;
using Repositories.Polls;

namespace WepApi.Installers
{
    public class RepositoriesInstallers : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IPollsRepository, PollsRepository>();
            services.AddScoped<INomineeRepository, NomineeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}
