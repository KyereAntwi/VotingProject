using Contracts.Responses.V1;
using MediatR;
using Repositories.Nominees;
using Repositories.Querries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Handlers
{
    public class GetAllNomineesHandler : IRequestHandler<GetAllNomineesQuery, List<NomineeResponse>>
    {
        private readonly INomineeRepository _nomineeRepo;

        public GetAllNomineesHandler(INomineeRepository nomineeRepository)
        {
            _nomineeRepo = nomineeRepository;
        }

        public async Task<List<NomineeResponse>> Handle(GetAllNomineesQuery request, CancellationToken cancellationToken)
        {
            var nominees = await _nomineeRepo.GetAllNomineesAsync();
            List<NomineeResponse> responses = new List<NomineeResponse>();
            if (nominees.Count > 0) 
            {
                foreach (var nom in nominees) 
                {
                    var newNominee = new NomineeResponse
                    {
                        Id = nom.Id,
                        Fullname = nom.Fullname,
                        ImageUrl = nom.ImageUrl
                    };

                    if (nom.CategoryNomineeDtos.Count > 0) 
                    {
                        List<CategoryResponse> categories = new List<CategoryResponse>();
                        foreach (var cat in nom.CategoryNomineeDtos) 
                        {
                            categories.Add(new CategoryResponse
                            {
                                Id = cat.Id
                            });
                        }
                    }

                    responses.Add(newNominee);
                }
            }

            return responses;
        }
    }
}
