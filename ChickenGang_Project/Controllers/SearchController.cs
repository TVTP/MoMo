using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChickenGang_Project.Models;

namespace ChickenGang_Project.Controllers
{
    public class SearchController : Controller
    {
        dbChickenGang db = new dbChickenGang();
        // GET: Search
        public ActionResult KQTimKiem(string sKey)
        {
            var lstSP = db.SanPhams.Where(n => n.TenSP.Contains(sKey));
            ViewBag.Key = sKey;
            return View(lstSP);


        }
    }
}