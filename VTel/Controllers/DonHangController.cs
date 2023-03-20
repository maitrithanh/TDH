using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTel.Models;

namespace VTel.Controllers
{
    public class DonHangController : Controller
    {
        VTELEntities database = new VTELEntities();
        // GET: DonHang
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["ROLEUSER"] != null && Convert.ToString(Session["ROLEUSER"]) == "Admin")
            {
                return View();
            }
            return Redirect("/LoginUser/Index");
        }
        [HttpPost]
        public ActionResult Create(DONHANG dh, FormCollection form)
        {
            try
            {
                CHITIETDONHANG chitietdonghang = new CHITIETDONHANG();
                chitietdonghang.IDDH = dh.IDDH;
                chitietdonghang.PRODUCTID = int.Parse(form["idsp"]);
                chitietdonghang.SOLUONG = int.Parse(form["soluong"]);
                chitietdonghang.DONGIA = int.Parse(form["dongia"]);
                chitietdonghang.THANHTIEN = int.Parse(form["soluong"]) * int.Parse(form["dongia"]);
                database.CHITIETDONHANGs.Add(chitietdonghang);
                dh.NGAYDAT = DateTime.Now;
                database.DONHANGs.Add(dh);
                //cap nhat hoa don
                HOADON _hoaDon = new HOADON();
                _hoaDon.PRODUCTID = int.Parse(form["idsp"]);
                _hoaDon.SOLUONG = int.Parse(form["soluong"]);
                _hoaDon.DONGIA = (decimal)Convert.ToDecimal(form["dongia"]);
                _hoaDon.MAKH = dh.MAKH;
                _hoaDon.HOTENKH = dh.MAKH;
                _hoaDon.MANV = "ADMIN";
                _hoaDon.NGAYTAO = DateTime.Now;
                _hoaDon.THANHTIEN = int.Parse(form["soluong"]) * int.Parse(form["dongia"]);
                database.HOADONs.Add(_hoaDon);
                database.SaveChanges();
                return Redirect("/Management/QuanLyDonHang");
            }
            catch (Exception e)
            {
                ViewBag.ErrorCustomerCreate = "Lỗi Tạo Mới";
                return Content(e.ToString());
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            if (Session["ROLEUSER"] != null && Convert.ToString(Session["ROLEUSER"]) == "Admin")
            {
                return View(database.CHITIETDONHANGs.Where(s => s.IDDH == id).FirstOrDefault());
            }
            return Redirect("/LoginUser/Index");
        }

        public ActionResult Edit(int id)
        {
            if (Session["ROLEUSER"] != null && Convert.ToString(Session["ROLEUSER"]) == "Admin")
            {
                return View(database.DONHANGs.Where(s => s.IDDH == id).FirstOrDefault());
            }
            return Redirect("/LoginUser/Index");
        }
        [HttpPost]
        public ActionResult Edit(int id, DONHANG dh)
        {
            try
            {
                database.Entry(dh).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return Redirect("/Management/QuanLyDonHang");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        public ActionResult EditCTDH(int id)
        {
            if (Session["ROLEUSER"] != null && Convert.ToString(Session["ROLEUSER"]) == "Admin")
            {
                return View(database.CHITIETDONHANGs.Where(s => s.IDDH == id).FirstOrDefault());
            }
            return Redirect("/LoginUser/Index");
        }
        [HttpPost]
        public ActionResult EditCTDH(int id, CHITIETDONHANG ctdh)
        {
            try
            {
                //HOADON _hoaDon = new HOADON();
                //_hoaDon.THANHTIEN = ctdh.SOLUONG * ctdh.DONGIA;
                ctdh.THANHTIEN = ctdh.SOLUONG * ctdh.DONGIA;
                //database.Entry(_hoaDon).State = System.Data.Entity.EntityState.Modified;
                database.Entry(ctdh).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return Redirect("/Management/QuanLyDonHang");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            return View(database.DONHANGs.Where(s => s.IDDH == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int id, DONHANG dh)
        {
            try
            {
                dh = database.DONHANGs.Where(s => s.IDDH == id).FirstOrDefault();
                CHITIETDONHANG ctdh = database.CHITIETDONHANGs.Where(s => s.IDDH == id).FirstOrDefault();
                if(ctdh != null)
                {
                    database.CHITIETDONHANGs.Remove(ctdh);
                }
                database.DONHANGs.Remove(dh);
                database.SaveChanges();
                return Redirect("/Management/QuanLyDonHang");
            }
            catch
            {
                return Content("Dữ Liệu Đang Được Sử Dụng! Không Thể Xoá");
            }
        }
    }
}