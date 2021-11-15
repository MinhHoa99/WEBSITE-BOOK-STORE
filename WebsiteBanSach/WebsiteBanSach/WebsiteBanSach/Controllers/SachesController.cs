using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.DAL;
using WebsiteBanSach.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;
namespace WebsiteBanSach.Controllers
{
    public class SachesController : Controller
    {
        private readonly Model1 db = new Model1();

        // GET: Saches
        public ActionResult Index(string searchString, int? giaMin, int? giaMax, string currentFilter, string sortOrder, int? page)
        {
            var saches = (from l in db.Saches select l);
            ViewBag.currentSort = sortOrder;
            ViewBag.sortName = String.IsNullOrEmpty(sortOrder) ? "Name_Desc" : "";
            ViewBag.sortAuthorName = sortOrder == "Author" ? "Author_Desc" : "Author";
            ViewBag.sortPrice = sortOrder == "price" ? "price_desc" : "price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                currentFilter = searchString;
            }

            ViewBag.currentFilter = searchString;

            //Tim kiem theo ten sach, tac gia, gia max min
            if (!String.IsNullOrEmpty(searchString))
            {
                saches = saches.Where(x => x.TenSach.Contains(searchString) || x.TacGia.Contains(searchString)
                || x.GiaSach >= giaMin && x.GiaSach <= giaMax);
            }

            //Sap xep
            switch (sortOrder)
            {
                case "Name_Desc":
                    saches = saches.OrderByDescending(x => x.TenSach);
                    break;
                case "Author_Desc":
                    saches = saches.OrderByDescending(x => x.TacGia);
                    break;
                case "Author":
                    saches = saches.OrderBy(x => x.TacGia);
                    break;
                case "price_desc":
                    saches = saches.OrderByDescending(x => x.GiaSach);
                    break;
                case "price":
                    saches = saches.OrderBy(x => x.GiaSach);
                    break;
                default:
                    saches = saches.OrderBy(x => x.TenSach);
                    break;
            }

            //Phan trang
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //IQueryable<Sach> list = (from sa in db.Saches select sa).OrderBy(s => s.TenSach);
            //saches = db.Saches.Include(s => s.CongTyPhatHanh).Include(s => s.DanhMuc).Include(s => s.NhaXuatBan);
            return View(saches.ToList().ToPagedList(pageNumber, pageSize));
        }


        // GET: Saches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // GET: Saches/Create
        public ActionResult Create()
        {
            ViewBag.MaCongTyPhatHanh = new SelectList(db.CongTyPhatHanhs, "MaCongTyPhatHanh", "TenCongTy");
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaNhaXuatBan = new SelectList(db.NhaXuatBans, "MaNhaXuatBan", "TenNXB");
            return View();
        }

        // POST: Saches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSach,TenSach,GiaSach,TacGia,LoaiBia,NgayXuatBan,MaNhaXuatBan,MaCongTyPhatHanh,MaDanhMuc,SoTrang,KichThuoc,MoTa,Hinh,SoLuongSach")] Sach sach, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {   
                    string _file = Path.GetFileName(img.FileName);
                    sach.Hinh = _file;
                    string _path = Path.Combine(Server.MapPath("~/HinhAnh"), _file);
                    img.SaveAs(_path);
                }
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCongTyPhatHanh = new SelectList(db.CongTyPhatHanhs, "MaCongTyPhatHanh", "TenCongTy", sach.MaCongTyPhatHanh);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sach.MaDanhMuc);
            ViewBag.MaNhaXuatBan = new SelectList(db.NhaXuatBans, "MaNhaXuatBan", "TenNXB", sach.MaNhaXuatBan);
            return View(sach);
        }

        // GET: Saches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCongTyPhatHanh = new SelectList(db.CongTyPhatHanhs, "MaCongTyPhatHanh", "TenCongTy", sach.MaCongTyPhatHanh);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sach.MaDanhMuc);
            ViewBag.MaNhaXuatBan = new SelectList(db.NhaXuatBans, "MaNhaXuatBan", "TenNXB", sach.MaNhaXuatBan);
            return View(sach);
        }

        // POST: Saches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSach,TenSach,GiaSach,TacGia,LoaiBia,NgayXuatBan,MaNhaXuatBan,MaCongTyPhatHanh,MaDanhMuc,SoTrang,KichThuoc,MoTa,Hinh,SoLuongSach")] Sach sach, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {
                    string _file = Path.GetFileName(img.FileName);
                    sach.Hinh = _file;
                    string _path = Path.Combine(Server.MapPath("~/HinhAnh"), _file);
                    img.SaveAs(_path);
                }
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCongTyPhatHanh = new SelectList(db.CongTyPhatHanhs, "MaCongTyPhatHanh", "TenCongTy", sach.MaCongTyPhatHanh);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sach.MaDanhMuc);
            ViewBag.MaNhaXuatBan = new SelectList(db.NhaXuatBans, "MaNhaXuatBan", "TenNXB", sach.MaNhaXuatBan);
            return View(sach);
        }

        // GET: Saches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sach sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
