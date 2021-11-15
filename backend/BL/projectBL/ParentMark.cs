using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class ParentMark
    {
        public parents par { get; set; }
       public double avg { get; set; }
       

        public ParentMark(parents p, double avg)
        {
            this.par = p;
            this.avg = avg;
        }
    }
}
