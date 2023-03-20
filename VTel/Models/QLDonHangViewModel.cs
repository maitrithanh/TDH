using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTel.Models
{
    public class QLDonHangViewModel
    {
        public int? IDDonHang { get; set; }
        public string NgayDat { get; set; }
        public string HoTenKhachHang { get; set; }
        public string DiaChi { get; set; }
        public string SDTKhachHang { get; set; }
        public string IDSanPham { get; set; }
        public int SoLuong { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public string Total_Money { get; set; }
        public HOADON HOADON { get; set; }
        public DONHANG DONHANG { get; set; }
        public virtual ICollection<DONHANG> DONHANGs { get; set; }
        public virtual ICollection<HOADON> HOADONs { get; set; }


    }
}