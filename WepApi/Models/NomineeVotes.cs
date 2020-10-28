using DTOs.DTOs;

namespace WepApi.Models
{
    public class NomineeVotes
    {
        public NomineeDto Nominee { get; set; }
        public int TotalVote { get; set; }
    }
}
