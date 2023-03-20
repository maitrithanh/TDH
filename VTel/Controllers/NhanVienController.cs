using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTel.Models;

namespace VTel.Controllers
{
    public class NhanVienController : Controller
    {
        VTELEntities database = new VTELEntities();
        // GET: NhanVien
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
        public ActionResult Create(NHANVIEN nv)
        {
            try
            {
                //AdminUser _user = new AdminUser();
                //_user.NAMEUSER = nv.MANV;
                //_user.PASSWORDUSER = nv.MANV;
                //_user.ROLEUSER = "Admin";
                //database.AdminUsers.Add(_user);
                //nv.USERID = _user.ID;
                database.NHANVIENs.Add(nv);
                database.SaveChanges();
                return Redirect("/Management/QuanLyNhanVien");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }
        public ActionResult Details(string id)
        {
            return View(database.NHANVIENs.Where(s => s.MANV == id).FirstOrDefault());
        }

        public ActionResult Edit(string id)
        {
            return View(database.NHANVIENs.Where(s => s.MANV == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(string id, NHANVIEN nv)
        {
            database.Entry(nv).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return Redirect("/Management/QuanLyNhanVien");
        }
        public ActionResult EditProfile(int id)
        {
            return View(database.NHANVIENs.Where(s => s.USERID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditProfile(int id, NHANVIEN nv)
        {
            database.Entry(nv).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return View();
        }
        public ActionResult Delete(string id)
        {
            var log = database.NHANVIENs.Where(s => s.MANV == id).FirstOrDefault();
            return View(log);
        }

        [HttpPost]
        public ActionResult Delete(string id, NHANVIEN nv)
        {
            try
            {
                nv = database.NHANVIENs.Where(s => s.MANV == id).FirstOrDefault();
                database.NHANVIENs.Remove(nv);
                database.SaveChanges();
                return Redirect("/Management/QuanLyNhanVien");
            }
            catch
            {
                return Content("Dữ Liệu Đang Được Sử Dụng! Không Thể Xoá");
            }
        }
    }
}