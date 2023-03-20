using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTel.Models
{
    public class ViewModel
    {
        public string TenSP { get; set; }
        public string ImgPro { get; set; }
        public decimal GiaSP { get; set; }
        public string TenDanhMuc { get; set; }
        public string DesPro { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public int? IDpro { get; set; }
        public decimal Total_Money { get; set; }
        public SANPHAM SANPHAM { get; set; }
        public DANHMUC DANHMUC { get; set; }
        public CHITIETDONHANG CHITIETDONHANG { get; set; }
        public IEnumerable<SANPHAM> DanhSachSanPham { get; set; }
        public IEnumerable<CHITIETDONHANG> CHITIETDONHANGs { get; set; }
        public int? Top5_Quantity { get; set; }
        public int? Sum_Quantity { get; set; }
    }
}