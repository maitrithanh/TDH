using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VTel.Models
{
    public class CartItem
    {
        public SANPHAM _SANPHAM { get; set; }
        public int _SOLUONG { get; set; }
    }
    public class Cart 
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        public void Add_Product_Cart(SANPHAM _sp, int _sl = 1)
        {
            var item = Items.FirstOrDefault(s => s._SANPHAM.PRODUCTID == _sp.PRODUCTID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _SANPHAM = _sp,
                    _SOLUONG = _sl
                });
            }
            else
            {
                item._SOLUONG += _sl;
            }
        }

        public int Total_Quantity()
        {
            return items.Sum(s => s._SOLUONG);
        }

        public decimal Total_Money()
        {
            var total = items.Sum(s => s._SOLUONG * s._SANPHAM.GIASP);
            return (decimal)total;
        }

        public void Update_Quantity(int id, int _sl_moi)
        {
            var item = items.Find(s => s._SANPHAM.PRODUCTID == id);
            if (item != null)
            {
                item._SOLUONG = _sl_moi;
                //if (items.Find(s => s._SANPHAM.SOLUONG > _sl_moi) != null)
                //{
                //    item._SOLUONG = _sl_moi;
                //} else
                //{
                //    item._SOLUONG = 1;
                //}
            }
        }

        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s._SANPHAM.PRODUCTID == id);
        }

        public void ClearCart()
        {
            items.Clear();
        }
    }
}