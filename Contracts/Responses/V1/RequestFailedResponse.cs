using System.Collections.Generic;

namespace Contracts.Responses.V1
{
    public class RequestFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
