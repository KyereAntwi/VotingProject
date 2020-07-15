using System;
using System.Collections.Generic;

namespace Contracts.Responses.V1
{
    public class NomineeResponse
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<CategoryResponse> Categories { get; set; }
    }
}
