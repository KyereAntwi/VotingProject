using DTOs.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Nominees
{
    public interface INomineeRepository
    {
        Task<List<NomineeDto>> GetAllNomineesAsync();
        Task<NomineeDto> GetASingleNomineeAsync(Guid Id);
        Task<NomineeDto> CreateANomineeAsync(NomineeDto nomineeDto);
        Task<NomineeDto> DeleteANomineeAsync(Guid Id);
    }
}
