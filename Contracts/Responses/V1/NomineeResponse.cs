using System;

namespace Contracts.Responses.V1
{
    public class NomineeResponse
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public Uri ImageUrl { get; set; }
    }
}
