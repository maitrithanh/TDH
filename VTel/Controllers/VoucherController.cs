using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTel.Models;

namespace VTel.Controllers
{
    public class VoucherController : Controller
    {
        VTELEntities database = new VTELEntities();
        // GET: Customers
        public ActionResult Index()
        {
            var Log = database.VOUCHERs.ToList();
            return View(Log);
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
        public ActionResult Create(VOUCHER voucher)
        {
            try
            {
                database.VOUCHERs.Add(voucher);
                database.SaveChanges();
                return Redirect("/Management/QuanLyVoucher");
            }
            catch
            {
                ViewBag.ErrorCustomerCreate = "Lỗi Tạo Mới";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (Session["ROLEUSER"] != null && Convert.ToString(Session["ROLEUSER"]) == "Admin")
            {
                return View(database.VOUCHERs.Where(s => s.IDVOUCHER == id).FirstOrDefault());
            }
            return Redirect("/LoginUser/Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["ROLEUSER"] != null && Convert.ToString(Session["ROLEUSER"]) == "Admin")
            {
                return View(database.VOUCHERs.Where(s => s.IDVOUCHER == id).FirstOrDefault());
            }
            return Redirect("/LoginUser/Index");
        }
        [HttpPost]
        public ActionResult Edit(VOUCHER vc)
        {
            try
            {
                database.Entry(vc).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return Redirect("/Management/QuanLyVoucher");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }
        public ActionResult Delete(int id)
        {
            return View(database.VOUCHERs.Where(s => s.IDVOUCHER == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int id, VOUCHER vc)
        {
            try
            {
                vc = database.VOUCHERs.Where(s => s.IDVOUCHER == id).FirstOrDefault();
                database.VOUCHERs.Remove(vc);
                database.SaveChanges();
                return Redirect("/Management/QuanLyVoucher");
            }
            catch
            {
                return Content("Dữ Liệu Đang Được Sử Dụng! Không Thể Xoá");
            }
        }

    }
}