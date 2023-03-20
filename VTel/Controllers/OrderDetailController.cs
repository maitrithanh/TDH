using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTel.Models;

namespace VTel.Controllers
{
    public class OrderDetailController : Controller
    {
        VTELEntities database = new VTELEntities();
        // GET: OrderDetail
        public ActionResult GroupByTop5()
        {
            List<CHITIETDONHANG> chiTietDH = database.CHITIETDONHANGs.ToList();
            List<SANPHAM> dsSanPham = database.SANPHAMs.ToList();

            var query = from od in chiTietDH join p in dsSanPham on od.PRODUCTID equals p.PRODUCTID into tbl
                        group od by new { idPro = od.PRODUCTID,
                            tenSp = od.SANPHAM.TENSP,
                            imagePro = od.SANPHAM.HINHANHSP,
                            giaSp = od.SANPHAM.GIASP
                        } into gr
                        orderby gr.Sum(s => s.SOLUONG) descending
                        select new ViewModel
                        {
                            IDpro = gr.Key.idPro,
                            TenSP = gr.Key.tenSp,
                            ImgPro = gr.Key.imagePro,
                            GiaSP = (decimal)gr.Key.giaSp,
                            Sum_Quantity = gr.Sum(s => s.SOLUONG)
                        };
                        
            return View(query.Take(5).ToList());
        }
    }
}