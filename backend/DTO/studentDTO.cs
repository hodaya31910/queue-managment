using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public partial class studentDTO
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string id_parent { get; set; }
        public int code_class { get; set; }
        public List<TimeSpan> lt { get; set; }
        public string nameclass { get; set; }
       
    }
}
