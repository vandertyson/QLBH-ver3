//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebService3
{
    using System;
    using System.Collections.Generic;
    
    public partial class GD_SAN_PHAM_UA_THICH
    {
        public decimal ID { get; set; }
        public decimal ID_TAI_KHOAN { get; set; }
        public decimal ID_HANG_HOA { get; set; }
    
        public virtual DM_HANG_HOA DM_HANG_HOA { get; set; }
        public virtual DM_TAI_KHOAN DM_TAI_KHOAN { get; set; }
    }
}
