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
    
    public partial class GD_TAG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GD_TAG()
        {
            this.GD_HANG_HOA_TAG = new HashSet<GD_HANG_HOA_TAG>();
            this.GD_HOA_DON_CHI_TIET = new HashSet<GD_HOA_DON_CHI_TIET>();
            this.GD_LOAI_TAG_CHI_TIET = new HashSet<GD_LOAI_TAG_CHI_TIET>();
            this.GD_PHIEU_NHAP_XUAT_CHI_TIET = new HashSet<GD_PHIEU_NHAP_XUAT_CHI_TIET>();
            this.GD_TON_KHO = new HashSet<GD_TON_KHO>();
        }
    
        public decimal ID { get; set; }
        public string TEN_TAG { get; set; }
        public string LINK_ANH { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GD_HANG_HOA_TAG> GD_HANG_HOA_TAG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GD_HOA_DON_CHI_TIET> GD_HOA_DON_CHI_TIET { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GD_LOAI_TAG_CHI_TIET> GD_LOAI_TAG_CHI_TIET { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GD_PHIEU_NHAP_XUAT_CHI_TIET> GD_PHIEU_NHAP_XUAT_CHI_TIET { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GD_TON_KHO> GD_TON_KHO { get; set; }
    }
}
