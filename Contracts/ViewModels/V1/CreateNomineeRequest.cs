using Microsoft.AspNetCore.Http;

namespace Contracts.ViewModels.V1
{
    public class CreateNomineeRequest
    {
        public string Fullname { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
