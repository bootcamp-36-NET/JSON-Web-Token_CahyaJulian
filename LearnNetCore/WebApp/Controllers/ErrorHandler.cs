using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("/Error/{status}")]
    public class ErrorHandler : Controller
    {
        public IActionResult HttpStatusCodeHandler(int status)
        {
            if (status == 404)
            {
                ViewBag.msg = "Sorry, the page doesn't exist";
            }
            return View("ErrorHandler");
        }
    }
}
