 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTel.Models;
using PagedList;
using PagedList.Mvc;

namespace VTel.Controllers
{
    public class ProductsController : Controller
    {
        VTELEntities database = new VTELEntities();
        // GET: Products
        public ActionResult Index(string danhmuc, int? page, string timkiem)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            if (timkiem != null)
            {
                var returnviewTimKiem = database.SANPHAMs.OrderByDescending(s => s.TENSP).Where(s => s.TENSP.Contains(timkiem));
                return View(returnviewTimKiem.ToPagedList(pageNum, pageSize));
            }
            if (danhmuc == null)
            {
                var productlist = database.SANPHAMs.OrderByDescending(x => x.TENSP);
                return View(productlist.ToPagedList(pageNum, pageSize));
            } else
            {
                var productlist = database.SANPHAMs.OrderByDescending(x => x.TENSP).Where(x => x.MADANHMUC == danhmuc);
                return View(productlist.ToPagedList(pageNum, pageSize));
            }
        }

        public ActionResult SearchOption(double min = double.MinValue, double max = double.MaxValue)
        {
            var list = database.SANPHAMs.Where(p => (double)p.GIASP >= min && (double)p.GIASP <= max).ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            if (Session["ROLEUSER"] != null && Convert.ToString(Session["ROLEUSER"]) == "Admin")
            {
                return View();
            }
            return Redirect("/LoginUser/Index");
        }

        public ActionResult SelectDanhMuc()
        {
            DANHMUC se_danhmuc = new DANHMUC();
            se_danhmuc.ListDanhMuc = database.DANHMUCs.ToList<DANHMUC>();
            return PartialView(se_danhmuc);
        }
        [HttpPost]
        public ActionResult Create(SANPHAM sp)
        {
            try
            {
                if (sp.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(sp.UploadImage.FileName);
                    string extent = Path.GetExtension(sp.UploadImage.FileName);
                    filename = filename + extent;
                    sp.HINHANHSP = "~/Content/images/" + filename;
                    sp.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                
                database.SANPHAMs.Add(sp);
                database.SaveChanges();
                return Redirect("/Management/QuanLySanPham");
            } 
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            return View(database.SANPHAMs.Where(s => s.PRODUCTID == id).FirstOrDefault());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["ROLEUSER"] != null && Convert.ToString(Session["ROLEUSER"]) == "Admin")
            {
                return View(database.SANPHAMs.Where(s => s.PRODUCTID == id).FirstOrDefault());
            }
            return Redirect("/LoginUser/Index");
        }
        [HttpPost]
        public ActionResult Edit(int id, SANPHAM sp)
        {
            try
            {
                if (sp.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(sp.UploadImage.FileName);
                    string extent = Path.GetExtension(sp.UploadImage.FileName);
                    filename = filename + extent;
                    sp.HINHANHSP = "~/Content/images/" + filename;
                    sp.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }

                database.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return Redirect("/Management/QuanLySanPham");
            }
            catch
            {
                ViewBag.ErrorProductCreate = "Lỗi Chỉnh Sửa";
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var log = database.SANPHAMs.Where(s => s.PRODUCTID == id).FirstOrDefault();
            return View(log);
        }

        [HttpPost]
        public ActionResult Delete(int id, SANPHAM sp)
        {
            try
            {
                sp = database.SANPHAMs.Where(s => s.PRODUCTID == id).FirstOrDefault();
                database.SANPHAMs.Remove(sp);
                database.SaveChanges();
                return Redirect("/Management/QuanLySanPham");
            }
            catch
            {
                return Content("Dữ Liệu Đang Được Sử Dụng! Không Thể Xoá");
            }
        }
    }
}