using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WepApi.Controllers.MVC
{
    public class ResultController : Controller
    {
        public ResultController()
        {

        }

        [HttpGet("/Result")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Polls List";
            return View();
        }
    }
}
