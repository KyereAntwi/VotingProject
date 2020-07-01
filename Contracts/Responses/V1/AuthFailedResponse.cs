using System.Collections.Generic;

namespace Contracts.Responses.V1
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
