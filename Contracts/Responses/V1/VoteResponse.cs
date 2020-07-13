using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Responses.V1
{
    public class VoteResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public Guid NomineeDtoId { get; set; }
        public Guid CategoryDtoId { get; set; }
    }
}
