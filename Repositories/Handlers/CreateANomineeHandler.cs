using Contracts.Responses.V1;
using DTOs.DTOs;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Repositories.Commands;
using Repositories.Nominees;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class CreateANomineeHandler : IRequestHandler<CreateANomineeCommand, NomineeResponse>
    {
        private readonly INomineeRepository _repository;
        private readonly ILogger<CreateANomineeHandler> _logger;
        private readonly IWebHostEnvironment _env;

        public CreateANomineeHandler(INomineeRepository repository, 
                                     ILogger<CreateANomineeHandler> logger,
                                     IWebHostEnvironment env)
        {
            _repository = repository;
            _logger = logger;
            _env = env;
        }

        public async Task<NomineeResponse> Handle(CreateANomineeCommand request, CancellationToken cancellationToken)
        {
            var nominee = new NomineeDto 
            {
                Fullname = request.Fullname
            };

            if (request.Avatar != null)
            {
                var filePath = Path.Combine(_env.WebRootPath, RandomName());
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write)) 
                {
                    await request.Avatar.CopyToAsync(fileStream);
                }
                nominee.ImageUrl = filePath;
            }

            var result = await _repository.CreateANomineeAsync(nominee);
            _logger.LogInformation($"A new nominee of Id: {result.Id} was created");

            return new NomineeResponse 
            {
                Fullname = result.Fullname,
                ImageUrl = result.ImageUrl
            };
        }

        private string RandomName(string prifix = "") => 
            $"img{prifix}_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.png";
    }
}
