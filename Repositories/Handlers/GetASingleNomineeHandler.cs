using Contracts.Responses.V1;
using MediatR;
using Repositories.Nominees;
using Repositories.Querries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class GetASingleNomineeHandler : IRequestHandler<GetASingleNomineeQuery, NomineeResponse>
    {
        private readonly INomineeRepository _nomRepo;

        public GetASingleNomineeHandler(INomineeRepository nomineeRepository)
        {
            _nomRepo = nomineeRepository;
        }

        public async Task<NomineeResponse> Handle(GetASingleNomineeQuery request, CancellationToken cancellationToken)
        {
            var nominee = await _nomRepo.GetASingleNomineeAsync(request.Id);

            if (nominee == null)
                return null;

            NomineeResponse response = new NomineeResponse();
            response.Id = nominee.Id;
            response.Fullname = nominee.Fullname;
            response.ImageUrl = nominee.ImageUrl;

            if (nominee.CategoryNomineeDtos.Count > 0) 
            {
                List<CategoryResponse> categories = new List<CategoryResponse>();
                foreach (var cat in nominee.CategoryNomineeDtos) 
                {
                    categories.Add(new CategoryResponse 
                    {
                        Id = cat.Id
                    });
                }
            }

            return response;
        }
    }
}
