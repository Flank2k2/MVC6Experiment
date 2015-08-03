using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6Experiment.Model
{
    public class Section
    {
        public Section()
        {
            Fields = new List<Field>();
        }

        public String Title { get; set; }
        public List<Field> Fields { get; set; }
    }
}
