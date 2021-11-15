using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public partial class requestDTO
    {
        public int code { get; set; }
        public string id_parent { get; set; }
        public System.TimeSpan from_hour { get; set; }
        public System.TimeSpan to_hour { get; set; }

    }
}
