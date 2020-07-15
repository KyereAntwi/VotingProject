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
            services.AddMediatR(typeof(AddPollHandler));
            services.AddMediatR(typeof(GetAllPollsHandler));
            services.AddMediatR(typeof(GetPollByIdHandler));
            services.AddMediatR(typeof(DeletePollHandler));
            services.AddMediatR(typeof(AddNomineeToCategoryHandler));
            services.AddMediatR(typeof(CreateCategoryHandler));
            services.AddMediatR(typeof(GetAllCategoriesOfPollHandler));
            services.AddMediatR(typeof(GetCategoryByIdHandler));
            services.AddMediatR(typeof(RemoveCategoryHandler));
            services.AddMediatR(typeof(RemoveNomineeFromCategoryHandler));
            services.AddMediatR(typeof(CreateANomineeHandler));
            services.AddMediatR(typeof(GetAllNomineesHandler));
            services.AddMediatR(typeof(GetASingleNomineeHandler));
            services.AddMediatR(typeof(DeleteNomineeHandler));
            services.AddMediatR(typeof(VoteHandler));
        }
    }
}
