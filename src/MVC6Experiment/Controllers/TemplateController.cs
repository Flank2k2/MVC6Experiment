using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MVC6Experiment.ViewModel;
using Microsoft.Framework.Logging;
using MVC6Experiment.Repository;
using MVC6Experiment.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC6Experiment.Controllers
{
    public class TemplateController : Controller
    {
        private ILogger _logger;
        private IRepository _repository;

        public TemplateController(ILogger<TemplateController> logger, IRepository repo)
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

        public IActionResult Edit(String name)
        {
            var template = _repository.GetTemplate(name);

            if (template == null)
            {
                _logger.LogInformation("Template not found {name}", name);
                return HttpNotFound();
            }

            var model = new TemplateView()
            {
                Template = template,
            };


            return View(model);
        }

        public IActionResult New()
        {
            _logger.LogInformation("Create empty template");

            var model = new TemplateView()
            {
                Template = Template.DEFAULT,
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult New(TemplateView templateView)
        {
            if (ModelState.IsValid)
            {
                var template = templateView.Template;
                
                template.CreationTime = DateTime.UtcNow;
                template.Permission = Permission.GLOBAL;

                if (String.IsNullOrWhiteSpace(template.Author))
                    template.Author = "Default User";

                var id = _repository.SaveTemplate(template);

                return RedirectToAction("Index");
            }

            return View(templateView);
        }

        public IActionResult Review(String name)
        {
            var template = _repository.GetTemplate(name);

            if (template == null)
            {
                _logger.LogInformation("Template not found {name}", name);
                return HttpNotFound();
            }

            var model = new TemplateView()
            {
                Template = template,
            };

            return View(model);
        }
    }
}
