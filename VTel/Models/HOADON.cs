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
    using System.ComponentModel.DataAnnotations;

    public partial class HOADON
    {
        public int IDHOADON { get; set; }
        public int PRODUCTID { get; set; }
        public int SOLUONG { get; set; }
        public decimal DONGIA { get; set; }
        public string MAKH { get; set; }
        public string HOTENKH { get; set; }
        public string MANV { get; set; }
        public string diachi { get; set; }
        public string sdt { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> NGAYTAO { get; set; }
        public Nullable<double> THANHTIEN { get; set; }
    
        public virtual NHANVIEN NHANVIEN { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
        public virtual DONHANG DONHANG { get; set; }
        public virtual ICollection<DONHANG> DONHANGs { get; set; }
        public virtual CHITIETDONHANG CHITIETDONHANG { get; set; }
    }
}
