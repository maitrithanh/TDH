using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTel.Models;

namespace VTel.Controllers
{
    public class CustomersController : Controller
    {
        VTELEntities database = new VTELEntities();
        // GET: Customers
        public ActionResult Index(string _name)
        {
            var Log = database.KHACHHANGs.ToList();
            if(_name == null)
            {
                return View(Log);
            }
            else
            {
                return View(database.KHACHHANGs.Where(s => s.MAKH.Contains(_name)).ToList());
            }
                
        }

        [HttpGet]
        public ActionResult Create()
        {
            if(Session["ROLEUSER"] != null && Convert.ToString(Session["ROLEUSER"]) == "Admin")
            {
                return View();
            }
            return Redirect("/LoginUser/Index");
        }
        [HttpPost]
        public ActionResult Create(KHACHHANG kh)
        {
            try
            {
                database.KHACHHANGs.Add(kh);
                database.SaveChanges();
                return Redirect("/Management/QuanLyKhachHang");
            }
            catch
            {
                ViewBag.ErrorCustomerCreate = "Lỗi Tạo Mới";
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            return View(database.KHACHHANGs.Where(s => s.Id == id).FirstOrDefault());
        }

        public ActionResult Edit(int id)
        {
            return View(database.KHACHHANGs.Where(s => s.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, KHACHHANG kh)
        {
            database.Entry(kh).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return Redirect("/Management/QuanLyKhachHang");
        }
        public ActionResult EditProfile(int id)
        {
            return View(database.KHACHHANGs.Where(s => s.USERID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditProfile(int id, KHACHHANG kh)
        {
            database.Entry(kh).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return View();
        }
        public ActionResult Delete(int id)
        {
            var log = database.KHACHHANGs.Where(s => s.Id == id).FirstOrDefault();
            return View(log);
        }

        [HttpPost]
        public ActionResult Delete(int id, KHACHHANG kh)
        {
            try
            {
                kh = database.KHACHHANGs.Where(s => s.Id == id).FirstOrDefault();
                database.KHACHHANGs.Remove(kh);
                database.SaveChanges();
                return Redirect("/Management/QuanLyKhachHang");
            }
            catch
            {
                return Content("Dữ Liệu Đang Được Sử Dụng! Không Thể Xoá");
            }
        }

    }
}