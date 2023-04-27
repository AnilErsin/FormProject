using Case.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Entities.Entity
{
    public class Field:BaseClass
    {
        public bool Required { get; set; }
        public string Name { get; set; }
        public string dataType { get; set; }
        public int FormID { get; set; }
        public Form Form { get; set; }
    }
}
