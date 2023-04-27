using Case.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Entities.Entity
{
    public class Form:BaseClass
    {
        public string  Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }


        public List<Field> Fields { get; set; }

    }
   
}
