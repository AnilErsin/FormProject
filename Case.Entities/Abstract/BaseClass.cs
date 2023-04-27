using Case.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Entities.Abstract
{
    public abstract class BaseClass : IEntity
    {
        public int ID { get; set ; }
       
    }
}
