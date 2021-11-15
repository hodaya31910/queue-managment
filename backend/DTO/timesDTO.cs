using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
  public partial class timesDTO
    {
        public int code { get; set; }
        public System.TimeSpan from_hour { get; set; }
        public System.TimeSpan to_hour { get; set; }
        public int code_class { get; set; }
    }
}
