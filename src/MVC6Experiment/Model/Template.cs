using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6Experiment.Model
{
    public class Template
    {
        public String Id { get; set; }
        public String Author { get; set; }
        public DateTime CreationTime { get; set; }
        public String Title { get; set; }
        public String Permission { get; set; }
        public Header Header { get; set; }
        public Header ChangeDescription { get; set; }
        public Header Monitoring { get; set; }
    }

    public class Header
    {
        public String Title { get; set; }
        public List<Field> Fields { get; set; }

        public Header()
        {
            Fields = new List<Field>();
        }
    }
}
