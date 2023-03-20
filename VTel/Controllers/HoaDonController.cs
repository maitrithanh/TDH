using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTel.Models;
namespace VTel.Controllers
{
    public class HoaDonController : Controller
    {
        VTELEntities database = new VTELEntities();
        // GET: HoaDon
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
        public ActionResult Create(HOADON hd)
        {
            try
            {
                database.HOADONs.Add(hd);
                database.SaveChanges();
                return Redirect("/Management/QuanLyHoaDon");
            }
            catch
            {
                ViewBag.ErrorCustomerCreate = "Lỗi Tạo Mới";
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            return View(database.HOADONs.Where(s => s.IDHOADON == id).FirstOrDefault());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["ROLEUSER"] != null && Convert.ToString(Session["ROLEUSER"]) == "Admin")
            {
                return View(database.HOADONs.Where(s => s.IDHOADON == id).FirstOrDefault());
            }
            return Redirect("/LoginUser/Index");
        }
        [HttpPost]
        public ActionResult Edit(HOADON hd)
        {
            try
            {
                database.Entry(hd).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return Redirect("/Management/QuanLyHoaDon");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }
        public ActionResult Delete(int id)
        {
            return View(database.HOADONs.Where(s => s.IDHOADON == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int id, HOADON hd)
        {
            try
            {
                hd = database.HOADONs.Where(s => s.IDHOADON == id).FirstOrDefault();
                database.HOADONs.Remove(hd);
                database.SaveChanges();
                return Redirect("/Management/QuanLyHoaDon");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }
    }
}