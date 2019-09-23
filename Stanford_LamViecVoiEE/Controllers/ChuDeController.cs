using Stanford_LamViecVoiEE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_LamViecVoiEE.Controllers
{
    public class ChuDeController : Controller
    {
        // GET: ChuDe
        public ActionResult ListChuDe()
        {
            IQueryable<stanfCategory> lstChuDe = DataProvider.Entities.stanfCategories;

            return View(lstChuDe);
        }

    }
}