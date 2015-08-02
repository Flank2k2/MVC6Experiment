using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6Experiment.Model
{
    public class Field
    {
        public String DisplayName { get; set; }
        public String Name { get; set; }
        public String Value { get; set; }
        public String Icon { get; set; }
    }

    public enum FIELD_TYPE
    {
        UNKNOWN = 0,
        TEXT = 1,

    }
}
