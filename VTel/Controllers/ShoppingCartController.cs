using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTel.Models;

namespace VTel.Controllers
{
    public class ShoppingCartController : Controller
    {
        VTELEntities database = new VTELEntities();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
            {
                return View("EmptyCart");
            }
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }

        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if(cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult AddToCart(int id)
        {
            if(Session["NAMEUSER"] != null)
            {
                var _pro = database.SANPHAMs.SingleOrDefault(s => s.PRODUCTID == id);
                if (_pro != null)
                {
                    GetCart().Add_Product_Cart(_pro);
                }
                return RedirectToAction("ShowCart", "ShoppingCart");
            }
            ViewBag.ErrorLogin = "Bạn cần đăng nhập để mua hàng";
            return Redirect("/LoginUser/Index");
        }

        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["idSP"]);
            int _quantity = int.Parse(form["SlCart"]);
            cart.Update_Quantity(id_pro, _quantity);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        public PartialViewResult BagCart()
        {
            int total_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if(cart != null)
            {
                total_quantity_item = cart.Total_Quantity();
            }
            ViewBag.QuantityCart = total_quantity_item;
            return PartialView("BagCart");
        }

        public ActionResult CheckOut(FormCollection form)
        {
            int voucherInput = 0;
            if(Int32.Parse(form["voucher"]) != 0)
            {
                voucherInput = Int32.Parse(form["voucher"]);
            }
            try
            {
                Cart cart = Session["Cart"] as Cart;
                var chietKhau = (double)0;
                var logID = Convert.ToString(Session["IDUSER"]);
                var IDCurrentUser = 0;
                KHACHHANG _khachHang = new KHACHHANG();
                var logLuuTen = database.KHACHHANGs.Where(s => s.MAKH == logID).FirstOrDefault();
                var hoTenKhachHang = logLuuTen.HOTENKH;
                //var checkDelete = database.KHACHHANGs.Where(s => s.MAKH == logID).FirstOrDefault();

                DONHANG _donHang = new DONHANG();
                _donHang.NGAYDAT = DateTime.Now;
                _donHang.DIACHIGIAOHANG = form["diachi"];
                _donHang.MAKH = form["idkh"];
                _donHang.SDTKH = form["sdtkh"];
                database.DONHANGs.Add(_donHang);
                foreach(var item in cart.Items)
                {
                    //Thuật Toán Áp Dụng Mã Voucher
                    if (voucherInput != 0)
                    {
                        var checkID = database.KHACHHANGs.Where(s => s.MAKH == logID && s.VOUCHERID == voucherInput).FirstOrDefault();
                        if (checkID != null)
                        {
                            IDCurrentUser = checkID.Id;
                            var checkVoucher = database.VOUCHERs.Where(s => s.IDVOUCHER == voucherInput).FirstOrDefault();
                            if (checkVoucher != null)
                            {
                                chietKhau = (double)checkVoucher.CHIETKHAU;
                                _khachHang.MAKH = logID;
                                _khachHang.HOTENKH = checkID.HOTENKH;
                                _khachHang.SDTKH = checkID.SDTKH;
                                _khachHang.EMAIL = checkID.EMAIL;
                                _khachHang.USERID = checkID.USERID;
                                _khachHang.VOUCHERID = null;
                                database.Entry(_khachHang).State = System.Data.Entity.EntityState.Modified;
                            }

                        }
                    }
                    var thanhTien = (item._SOLUONG * (double)item._SANPHAM.GIASP) - ((double)item._SANPHAM.GIASP * chietKhau);
                    CHITIETDONHANG _chiTietDonHang = new CHITIETDONHANG();
                    _chiTietDonHang.IDDH = _donHang.IDDH;
                    _chiTietDonHang.PRODUCTID = item._SANPHAM.PRODUCTID;
                    _chiTietDonHang.DONGIA = (double)item._SANPHAM.GIASP;
                    _chiTietDonHang.SOLUONG = item._SOLUONG;
                    _chiTietDonHang.THANHTIEN = thanhTien;
                    //cap nhat hoa don
                    SANPHAM sanpham = new SANPHAM();
                    var checkSL = database.SANPHAMs.Where(s => s.PRODUCTID == item._SANPHAM.PRODUCTID).FirstOrDefault();
                    if(checkSL != null)
                    {
                        if(item._SOLUONG > checkSL.SOLUONG)
                        {
                            ViewBag.ErrorSoLuong = "Không đủ số lượng!";
                            Session["MaSpQuanTi"] = checkSL.TENSP;
                            Session["Alert"] = "Không đủ số lượng!";
                            return Redirect("/ShoppingCart/ShowCart");
                            //return View("ShowCart");
                        }
                    }
                    HOADON _hoaDon = new HOADON();
                    _hoaDon.PRODUCTID = item._SANPHAM.PRODUCTID;
                    _hoaDon.SOLUONG = item._SOLUONG;
                    _hoaDon.DONGIA = (decimal)item._SANPHAM.GIASP;
                    _hoaDon.MAKH = logID;
                    _hoaDon.HOTENKH = hoTenKhachHang;
                    _hoaDon.MANV = "ROOT";
                    _hoaDon.NGAYTAO = DateTime.Now;
                    _hoaDon.THANHTIEN = thanhTien;
                    database.HOADONs.Add(_hoaDon);
                    database.CHITIETDONHANGs.Add(_chiTietDonHang);
                    //cap nhat lai so luong hang con lai trong kho
                    foreach(var sp in database.SANPHAMs.Where(s => s.PRODUCTID == _chiTietDonHang.PRODUCTID))
                    {
                        var update_sl_sp = sp.SOLUONG - item._SOLUONG;
                        if(update_sl_sp > 0)
                        {
                            sp.SOLUONG = update_sl_sp;
                        } else
                        {
                            sp.SOLUONG = 0;
                        }

                    }
                }
                database.SaveChanges();
                cart.ClearCart();
                //cap nhat lai voucher
                //if (checkDelete != null)
                //{
                //    _khachHang.MAKH = _khachHang.MAKH;
                //    _khachHang.VOUCHERID = 2;
                //    database.Entry(_khachHang).State = System.Data.Entity.EntityState.Modified;
                //    database.SaveChanges();
                //}
                return RedirectToAction("CheckOut_Success", "ShoppingCart");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }

        public ActionResult CheckOut_Success()
        {
            return View();
        }
    }
}