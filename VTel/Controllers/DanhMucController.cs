 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VTel.Models;

namespace VTel.Controllers
{
    public class DanhMucController : Controller
    {
        VTELEntities database = new VTELEntities();
        // GET: DanhMuc
        public PartialViewResult DanhMucPartial()
        {
            var cateList = database.DANHMUCs.ToList();
            return PartialView(cateList);
        }

        public ActionResult Create()
        {
            if (Session["ROLEUSER"] != null && Convert.ToString(Session["ROLEUSER"]) == "Admin")
            {
                return View();
            }
            return Redirect("/LoginUser/Index");
        }
        [HttpPost]
        public ActionResult Create(DANHMUC cate)
        {
            try
            {
                database.DANHMUCs.Add(cate);
                database.SaveChanges();
                return Redirect("/Management/QuanLyDanhMuc");
            }
            catch
            {
                return Content("Lỗi tạo mới");
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (Session["ROLEUSER"] != null && Convert.ToString(Session["ROLEUSER"]) == "Admin")
            {
                return View(database.DANHMUCs.Where(s => s.ID == id).FirstOrDefault());
            }
            return Redirect("/LoginUser/Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["ROLEUSER"] != null && Convert.ToString(Session["ROLEUSER"]) == "Admin")
            {
                return View(database.DANHMUCs.Where(s => s.ID == id).FirstOrDefault());
            }
            return Redirect("/LoginUser/Index");
        }
        [HttpPost]
        public ActionResult Edit(DANHMUC cate)
        {
            try
            {
                database.Entry(cate).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return Redirect("/Management/QuanLyDanhMuc");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        public ActionResult Delete(int id)
        {
            return View(database.DANHMUCs.Where(s => s.ID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int id, DANHMUC cate)
        {
            try
            {
                cate = database.DANHMUCs.Where(s => s.ID == id).FirstOrDefault();
                database.DANHMUCs.Remove(cate);
                database.SaveChanges();
                return Redirect("/Management/QuanLyDanhMuc");
            }
            catch
            {
                return Content("Dữ Liệu Đang Được Sử Dụng! Không Thể Xoá");
            }
        }
    }
}