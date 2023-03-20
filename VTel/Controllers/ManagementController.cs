using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTel.Models;
namespace VTel.Controllers
{
    public class ManagementController : Controller
    {
        // GET: Management
        VTELEntities database = new VTELEntities();
        public ActionResult Index()
        {
            if(Convert.ToString(Session["ROLEUSER"]) != "Admin")
            {
                return Redirect("/LoginUser/Index");
            }
            var hoadonlist = database.HOADONs.ToList();
            var count = database.HOADONs.ToList().Count();
            Session["SoDonHang"] = count; 
            var doanhthu = database.HOADONs.Sum(s => s.THANHTIEN);
            Session["DoanhThu"] = doanhthu;
            var khachhang = database.KHACHHANGs.ToList().Count();
            Session["SoKhachHang"] = khachhang;
            var account = database.AdminUsers.ToList().Count();
            Session["SoAccount"] = account;
            return View(hoadonlist);
        }

        public ActionResult QuanLySanPham()
        {
            if(Convert.ToString(Session["ROLEUSER"]) != "Admin")
            {
                return Redirect("/LoginUser/Index");
            }
            var productlist = database.SANPHAMs.OrderByDescending(x => x.TENSP);
            return View(productlist);
        }

        public ActionResult QuanLyKhachHang(string _name)
        {
            if (Convert.ToString(Session["ROLEUSER"]) == "Admin")
            {
                var Log = database.KHACHHANGs.ToList();
                if (_name == null)
                {
                    return View(Log);
                }
                else
                {
                    return View(database.KHACHHANGs.Where(s => s.MAKH.Contains(_name)).ToList());
                }
            } else
            {
                return Redirect("/LoginUser/Index");
            }
        }
        [HttpGet]
        public ActionResult QuanLyDanhMuc()
        {
            if (Convert.ToString(Session["ROLEUSER"]) != "Admin")
            {
                return Redirect("/LoginUser/Index");
            }
            else
            {
                var cateList = database.DANHMUCs.ToList();
                return View(cateList);
            }
        }
        [HttpGet]
        public ActionResult QuanLyHoaDon()
        {
            if (Convert.ToString(Session["ROLEUSER"]) != "Admin")
            {
                return Redirect("/LoginUser/Index");
            }
            var hoadonlist = database.HOADONs.ToList();
            return View(hoadonlist);
        }
        [HttpGet]
        public ActionResult QuanLyVoucher()
        {
            if (Convert.ToString(Session["ROLEUSER"]) != "Admin")
            {
                return Redirect("/LoginUser/Index");
            }
            var voucherlist = database.VOUCHERs.ToList();
            return View(voucherlist);
        }

        [HttpGet]
        public ActionResult QuanLyNhanVien()
        {
            if (Convert.ToString(Session["ROLEUSER"]) != "Admin")
            {
                return Redirect("/LoginUser/Index");
            }
            var nhavienlist = database.NHANVIENs.ToList();
            return View(nhavienlist);
        }
        [HttpGet]
        public ActionResult QuanLyDonHang()
        {
            if (Convert.ToString(Session["ROLEUSER"]) != "Admin")
            {
                return Redirect("/LoginUser/Index");
            }

            var listquery = database.DONHANGs.ToList();

            return View(listquery);
        }

        [HttpGet]
        public ActionResult QuanLyDonHangKhachHang()
        {
            if(Convert.ToString(Session["IDUSERPROFILE"]) == null)
            {
                return Redirect("/LoginUser/Index");
            }
            string makh = Convert.ToString(Session["IDUSER"]);
            return View(database.DONHANGs.Where(s => s.MAKH.Contains(makh)).Take(5).ToList());
        }
    }
}