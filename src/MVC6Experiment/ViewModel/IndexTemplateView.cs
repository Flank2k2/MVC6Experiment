using MVC6Experiment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6Experiment.ViewModel
{
    public class IndexTemplateView
    {
        public IEnumerable<Template> Templates { get; set; }
    }
}
