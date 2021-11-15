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

namespace WebsiteBanSach.Controllers
{
    public class CongTyPhatHanhsController : Controller
    {
        private Model1 db = new Model1();

        // GET: CongTyPhatHanhs
        public ActionResult Index()
        {
            return View(db.CongTyPhatHanhs.ToList());
        }

        // GET: CongTyPhatHanhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongTyPhatHanh congTyPhatHanh = db.CongTyPhatHanhs.Find(id);
            if (congTyPhatHanh == null)
            {
                return HttpNotFound();
            }
            return View(congTyPhatHanh);
        }

        // GET: CongTyPhatHanhs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CongTyPhatHanhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCongTyPhatHanh,TenCongTy,SoLuongSach")] CongTyPhatHanh congTyPhatHanh)
        {
            if (ModelState.IsValid)
            {
                db.CongTyPhatHanhs.Add(congTyPhatHanh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(congTyPhatHanh);
        }

        // GET: CongTyPhatHanhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongTyPhatHanh congTyPhatHanh = db.CongTyPhatHanhs.Find(id);
            if (congTyPhatHanh == null)
            {
                return HttpNotFound();
            }
            return View(congTyPhatHanh);
        }

        // POST: CongTyPhatHanhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCongTyPhatHanh,TenCongTy,SoLuongSach")] CongTyPhatHanh congTyPhatHanh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(congTyPhatHanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(congTyPhatHanh);
        }

        // GET: CongTyPhatHanhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongTyPhatHanh congTyPhatHanh = db.CongTyPhatHanhs.Find(id);
            if (congTyPhatHanh == null)
            {
                return HttpNotFound();
            }
            return View(congTyPhatHanh);
        }

        // POST: CongTyPhatHanhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CongTyPhatHanh congTyPhatHanh = db.CongTyPhatHanhs.Find(id);
            db.CongTyPhatHanhs.Remove(congTyPhatHanh);
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
