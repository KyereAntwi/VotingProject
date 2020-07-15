using Contracts.Responses.V1;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Repositories.Commands
{
    public class CreateANomineeCommand : IRequest<NomineeResponse>
    {
        public string Fullname { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
