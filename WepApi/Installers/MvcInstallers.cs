using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WepApi.Installers
{
    public class MvcInstallers : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddControllers();
            services.AddAuthorization();
            services.AddMediatR(typeof(Startup));
        }
    }
}
