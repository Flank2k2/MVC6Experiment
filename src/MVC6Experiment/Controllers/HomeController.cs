using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;
using MVC6Experiment.Repository;

namespace MVC6Experiment.Controllers
{
    public class HomeController : Controller
    {
        private ILogger _logger;
        private IRepository _repository;

        public HomeController(ILogger<HomeController> logger, IRepository repo)
        {
            _logger = logger;
            _repository = repo;
        }

        public IActionResult Index()
        {
            var tt = "TROLOLOL !!";
            _logger.LogInformation("Enter index {var}", tt);


            return View();
        }

        public IActionResult Templates()
        {
            ViewBag.Message = "This is the template page !!";



            return View();
        }


        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
