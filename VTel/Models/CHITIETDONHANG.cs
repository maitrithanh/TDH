//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VTel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHITIETDONHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHITIETDONHANG()
        {
            this.HOADONs = new HashSet<HOADON>();
        }

        public int IDCHITIETDONHANG { get; set; }
        public Nullable<int> PRODUCTID { get; set; }
        public Nullable<int> IDDH { get; set; }
        public Nullable<int> SOLUONG { get; set; }
        public Nullable<double> DONGIA { get; set; }
        public Nullable<double> THANHTIEN { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
        public virtual DONHANG DONHANG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
