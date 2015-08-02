using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6Experiment.Model
{
    public class Client
    {
        public String Id { get; set; }
        public String DisplayName { get; set; }
        public String URL { get; set; }
        public String Hostname { get; set; }
        public String AddressIP { get; set; }
        public String Environment { get; set; }
        public String DetailsURL { get; set; }
        public Boolean IsHosted { get; set; }

    }
}
