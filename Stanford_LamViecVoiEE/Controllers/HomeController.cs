using Stanford_LamViecVoiEE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_LamViecVoiEE.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuanLy()
        {
            //Lấy danh sách bài viết
            var lstBaiViet = DataProvider.Entities.stanfArticles.OrderByDescending(p=>p.Id).ToList();

            return View(lstBaiViet);
        }
        public ActionResult demo()
        {
            //Khai báo 1 mảng
            int[] arr = new int[10] { 5, 20, 50, 10, 15, 40, 35, 65, 80, 95 };

            //Lấy các số chẵn
            var lstChan = from a in arr
                          where a % 2 == 0
                          select a;

            //Sắp tăng dần
            var lstTangDan =(from a in arr
                                            orderby a
                             select a);

            //Max, min
            int max = arr.Max();

            int min = arr.Min();

            return View(lstTangDan);
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