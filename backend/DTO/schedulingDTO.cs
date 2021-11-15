using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public partial class schedulingDTO
    {
        public int code { get; set; }
        public string id_student { get; set; }
        public string nameMother { get; set; }
        public string idMother { get; set; }
        public  string nameStudent { get; set; }
        public bool disableEnter { get; set; }
        public bool disableExit { get; set; }

        public int code_class { get; set; }
        public string class_ { get; set; }
        public System.TimeSpan hour_ { get; set; }
        public Nullable<System.TimeSpan> hour_reach { get; set; }
        public Nullable<System.TimeSpan> hour_enter { get; set; }
        public Nullable<System.TimeSpan> hour_exit { get; set; }
    }
}
