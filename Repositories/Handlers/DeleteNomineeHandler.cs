using Contracts.Responses.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using Repositories.Commands;
using Repositories.Nominees;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class DeleteNomineeHandler : IRequestHandler<DeleteNomineeCommand, NomineeResponse>
    {
        private readonly INomineeRepository _repository;
        private readonly ILogger<DeleteNomineeHandler> _logger;

        public DeleteNomineeHandler(INomineeRepository repository, ILogger<DeleteNomineeHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<NomineeResponse> Handle(DeleteNomineeCommand request, CancellationToken cancellationToken)
        {
            var nominee = await _repository.DeleteANomineeAsync(request.Id);

            if (nominee == null)
                return null;

            _logger.LogInformation($"A nominee with Id: {nominee.Id} was deleted");
            return new NomineeResponse 
            {
                Id = nominee.Id,
                Fullname = nominee.Fullname,
                ImageUrl = nominee.ImageUrl
            };
        }
    }
}
