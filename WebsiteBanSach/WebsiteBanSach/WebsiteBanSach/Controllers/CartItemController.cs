using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.DAL;
using WebsiteBanSach.Models;
namespace WebsiteBanSach.Controllers
{
    public class CartItemController : Controller
    {
        private Model1 db = new Model1();
        // GET: CartItem
        //[Authorize(Roles = "Admin, Customer")]
        public ActionResult Index()
        {
            List<Sach> products = Session["giohang"] as List<Sach>;
            return View(products);
        }

        public RedirectToRouteResult ThemVaoGio(int SanPhamID)
        {
            if (Session["giohang"] == null)
            {
                Session["giohang"] = new List<Sach>();
            }

            List<Sach> giohang = Session["giohang"] as List<Sach>;  //Gan bien gio hang 

            if (giohang.FirstOrDefault(m => m.MaSach == SanPhamID) == null)
            {
                Sach sp = db.Saches.Find(SanPhamID);

                Sach newItem = new Sach()
                {
                    MaSach = SanPhamID,
                    TenSach = sp.TenSach,
                    SoLuongSach = 1,
                    Hinh = sp.Hinh,
                    GiaSach = sp.GiaSach

                };

                giohang.Add(newItem);
            }
            else
            {
                Sach cardItem = giohang.FirstOrDefault(m => m.MaSach == SanPhamID);
                cardItem.SoLuongSach++;
            }
            return RedirectToAction("Index", "Home", new { id = SanPhamID });
        }
        public RedirectToRouteResult XoaKhoiGio(int SanPhamID)
        {
            List<Sach> giohang = Session["giohang"] as List<Sach>;
            Sach itemXoa = giohang.FirstOrDefault(m => m.MaSach == SanPhamID);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult SuaSoLuong(int SanPhamID, int soluongmoi)
        {
            // tìm carditem muon sua
            List<Sach> giohang = Session["giohang"] as List<Sach>;
            Sach itemSua = giohang.FirstOrDefault(m => m.MaSach == SanPhamID);
            if (itemSua != null)
            {
                itemSua.SoLuongSach = soluongmoi;
            }
            return RedirectToAction("Index");

        }
    }
}