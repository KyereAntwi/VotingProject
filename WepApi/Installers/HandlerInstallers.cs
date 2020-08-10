using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Handlers;

namespace WepApi.Installers
{
    public class HandlerInstallers : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            // handlers registrations
            services.AddMediatR(typeof(AddPollHandler).Assembly);
        }
    }
}
