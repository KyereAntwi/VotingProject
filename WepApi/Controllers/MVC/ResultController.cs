using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repositories.Polls;

namespace WepApi.Controllers.MVC
{
    public class ResultController : Controller
    {
        private readonly IPollsRepository _pollRepo;

        public ResultController(IPollsRepository pollsRepository)
        {
            _pollRepo = pollsRepository;
        }

        [HttpGet("/Result/{PollId}")]
        public async Task<IActionResult> Index(Guid PollId)
        {
            if (PollId == Guid.Empty)
                return View();

            var poll = await _pollRepo.GetSinglePollResponseAsync(PollId);

            ViewData["Title"] = $"{poll.Theme}";
            return View(poll);
        }
    }
}
