//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class times
    {
        public int code { get; set; }
        public System.TimeSpan from_hour { get; set; }
        public System.TimeSpan to_hour { get; set; }
        public int code_class { get; set; }
    
        public virtual classes classes { get; set; }
    }
}
