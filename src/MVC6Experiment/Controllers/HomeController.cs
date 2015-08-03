using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;
using MVC6Experiment.Repository;
using MVC6Experiment.ViewModel;
using MVC6Experiment.Model;

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
            var templates = _repository.GetAllTemplates();
            _logger.LogInformation("List all templates {count}", templates.Count());

            var model = new IndexTemplateView()
            {
                Templates = templates,
            };

            return View(model);
        }

        public IActionResult New(string id)
        {
            _logger.LogInformation("Get template {id}", id);

            var templates = _repository.GetTemplate(id);

            _logger.LogInformation("Remove client data fields");
            foreach (var field in templates.Header.Fields)
            {
                //TODO: Find something other than magic string
                if (field.Name == "head_starttime")
                {
                    field.Value = "23:00";
                }
                else if (field.Name == "head_proposeddate")
                {
                    field.Value = DateTime.Now.ToString("yyyy.MM.dd");
                }
                else
                {
                    field.Value = "";
                }
            }

            var model = new TemplateView()
            {
                Template = templates,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult New(TemplateView templateView)
        {
            if (ModelState.IsValid)
            {
                var template = templateView.Template;






                return RedirectToAction("Index");
            }

            return View(templateView);
        }


        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
