using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VTel.Models;

namespace VTel.Controllers
{
    public class LoginUserController : Controller
    {
        VTELEntities database = new VTELEntities();
        // GET: LoginUser

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult LoginUser(AdminUser _user)
        {

            var checkRole = database.AdminUsers.Where(s => s.NAMEUSER == _user.NAMEUSER && s.ROLEUSER == "Admin").FirstOrDefault();
           

            var check = database.AdminUsers.Where(s => s.NAMEUSER == _user.NAMEUSER).FirstOrDefault();
            bool checkFinal = false;
            if (check != null)
            {
                checkFinal = BCrypt.Net.BCrypt.Verify(_user.PASSWORDUSER, check.PASSWORDUSER);
            }
            if (check == null || checkFinal == false)
            {
                ViewBag.ErrorLogin = "Sai Tài Khoản Hoặc Mật Khẩu";
                return View("Index");
            }
            else
            {
                if (checkRole == null)
                {
                    Session["ROLEUSER"] = null;
                }
                else
                {
                    Session["ROLEUSER"] = "Admin";
                }
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["NAMEUSER"] = _user.NAMEUSER;
                Session["IDUSER"] = ("KH"+ check.ID);
                Session["IDUSERPROFILE"] = check.ID;

                //Set Voucher
                var logID = Convert.ToString(Session["IDUSER"]);
                var checkVoucherSession = database.KHACHHANGs.Where(s => s.MAKH == logID && s.VOUCHERID != null).FirstOrDefault();
                if (checkVoucherSession != null)
                {
                    Session["VOUCHERID"] = checkVoucherSession.VOUCHERID;
                    var checkVoucherNameSession = database.VOUCHERs.Where(s => s.IDVOUCHER == checkVoucherSession.VOUCHERID).FirstOrDefault();
                    if (checkVoucherNameSession != null)
                    {
                        Session["VOUCHERNAME"] = checkVoucherNameSession.TENVOUCHER;
                        Session["CHIETKHAU"] = checkVoucherNameSession.CHIETKHAU;
                    }
                }
                //END
                if (checkRole == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Management");
                }
            }
        }

        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(AdminUser _user)
        {
            if (ModelState.IsValid)
            {
                var check_ID = database.AdminUsers.Where(s => s.NAMEUSER == _user.NAMEUSER).FirstOrDefault();
                KHACHHANG _khachHang = new KHACHHANG();
                if (check_ID == null)
                {
                    int cost = 10;
                    string hasPassword = BCrypt.Net.BCrypt.HashPassword(_user.PASSWORDUSER, cost);
                    database.Configuration.ValidateOnSaveEnabled = false;
                    _user.PASSWORDUSER = hasPassword;
                    database.AdminUsers.Add(_user);
                    database.SaveChanges();
                    _khachHang.MAKH = "KH" + _user.ID;
                    _khachHang.HOTENKH = _user.NAMEUSER;
                    _khachHang.USERID = _user.ID;
                    _khachHang.VOUCHERID = 1;
                    database.KHACHHANGs.Add(_khachHang);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                } else
                {
                    ViewBag.ErrorRegister = "Tên Đăng Nhập Đã Tồn Tại";
                    return View();
                }
            }
            return View();
        }

        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}