using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanSach.DAL;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Controllers
{
    public class HomeController : Controller
    {
        private readonly Model1 db = new Model1();
        //public ActionResult Index()
        //{
        //    List<Sach> saches = db.Saches.ToList();
        //    return View(saches);

        //}
        

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

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(saches.ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}