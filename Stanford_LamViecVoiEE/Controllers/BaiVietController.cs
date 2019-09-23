using Stanford_LamViecVoiEE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace Stanford_LamViecVoiEE.Controllers
{
    public class BaiVietController : Controller
    {
        /// <summary>
        /// Hàm lấy danh sách bài viết
        /// </summary>
        /// <param name="tuKhoa"></param>
        /// <returns></returns>
        public ActionResult danhsach(string tuKhoa)
        {
            //Lấy danh sách bài viết
            IQueryable<stanfArticle> lstBaiViet = DataProvider.Entities.stanfArticles;

            //Nếu có từ khóa cần tìm kiếm
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                lstBaiViet = lstBaiViet.Where(p => p.Title.Contains(tuKhoa) || p.Description.Contains(tuKhoa));

            }

            ViewBag.tuKhoa = tuKhoa;
            return View(lstBaiViet.OrderByDescending(p => p.Id));
        }
        /// <summary>
        /// Hiển thị danh sách chủ đề bài viết
        /// </summary>
        /// <param name="chuDeId"></param>
        private void HienThiChuDeBaiViet(int? chuDeId)
        {
            //Lấy danh sách chủ đề
            List<stanfCategory> lstChuDe = DataProvider.Entities.stanfCategories.ToList();

            ViewBag.ChuDe = new SelectList(lstChuDe, "Id", "CategoryName", chuDeId.HasValue ? chuDeId.Value : 0);
        }

        public ActionResult ThemMoi()
        {
            HienThiChuDeBaiViet(0);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemMoi(stanfArticle objBaiViet, HttpPostedFileBase fUpload)
        {
            //Kiểm tra bằng code nếu TH tile null
            /*if(string.IsNullOrEmpty(objBaiViet.Title))
            {
                ModelState.AddModelError("Title", "Bạn cần phải nhập tiêu đề bài viết");
            }*/

            //Nếu không còn lỗi thì mới thực hiện công việc trong này
            if (ModelState.IsValid)
            {
                //Xử lý upload
                if (fUpload != null && fUpload.ContentLength > 0)
                {
                    fUpload.SaveAs(Server.MapPath("~/Content/images/" + fUpload.FileName));
                    objBaiViet.ImageName = fUpload.FileName;
                }

                objBaiViet.DateCreated = DateTime.Now;
                objBaiViet.DateLastUpdate = DateTime.Now;

                //Lưu thông tin vào db
                DataProvider.Entities.stanfArticles.Add(objBaiViet);
                DataProvider.Entities.SaveChanges();
            }
            HienThiChuDeBaiViet(0);

            return View();
        }

        public ActionResult SuaThongTin(int id)
        {
            //Lấy thông tin chi tiết bài viết
            stanfArticle objBaiViet = DataProvider.Entities.stanfArticles.Where(p => p.Id == id).First();

            HienThiChuDeBaiViet(objBaiViet.CategoryId);

            return View(objBaiViet);
        }

        /// <summary>
        /// Hàm xử lý cập nhật bài viết
        /// </summary>
        /// <param name="objBaiViet"></param>
        /// <param name="id"></param>
        /// <param name="fUpload"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaThongTin(stanfArticle objBaiViet, int id, HttpPostedFileBase fUpload)
        {
            //Lấy thông tin chi tiết bài viết cũ
            stanfArticle objBaiVietOld = DataProvider.Entities.stanfArticles.Where(p => p.Id == id).First();

            objBaiViet.DateLastUpdate = DateTime.Now;
            //Xử lý upload
            if (fUpload != null && fUpload.ContentLength > 0)
            {
                fUpload.SaveAs(Server.MapPath("~/Content/images/" + fUpload.FileName));
                objBaiViet.ImageName = fUpload.FileName;
            }

            DataProvider.Entities.Entry(objBaiVietOld).CurrentValues.SetValues(objBaiViet);

            //Lưu sự thay đổi
            DataProvider.Entities.SaveChanges();

            return RedirectToAction("danhsach");
        }

        /// <summary>
        /// Xóa thông tin bài viết
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult XoaBaiViet(int id)
        {
            //Lấy đối tượng bài viết cần xóa
            stanfArticle objBaiViet = DataProvider.Entities.stanfArticles.Where(p => p.Id == id).First();

            if (objBaiViet != null)
            {
                //Xóa khỏi model
                DataProvider.Entities.stanfArticles.Remove(objBaiViet);
                DataProvider.Entities.SaveChanges();//Lưu sự thay đổi
                return RedirectToAction("danhsach");
            }
            return View();
        }
        /// <summary>
        /// So luong ban ghi tren 1 trang
        /// </summary>
        int pageSize = 5;
        public ActionResult danhsachvm(int? page)
        {
            
            IQueryable<ViewArticleModel> lstBaiViet = (from a in DataProvider.Entities.stanfArticles
                                                       join c in DataProvider.Entities.stanfCategories on a.CategoryId                            equals c.Id
                                                       select new ViewArticleModel()
                                                       {
                                                           Id = a.Id,
                                                           Title = a.Title,
                                                           Description = a.Description,
                                                           AuthorName = a.AuthorName,
                                                           CategoryName = c.CategoryName,
                                                           DateCreated = a.DateCreated

                                                       });
            return View(lstBaiViet.OrderByDescending(a => a.DateCreated).ToPagedList((page.HasValue ? page.Value : 1), pageSize));
        }
    }
}