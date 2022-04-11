using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChickenGang_Project.Models;
using PagedList;

namespace ChickenGang_Project.Controllers
{
    public class SanPhamController : Controller
    {
        dbChickenGang db = new dbChickenGang();

        // GET: SanPham
        //-----------------Sản phầm---------------//
        public ActionResult SanPhamStyle1Partial()
        {
            return PartialView();
        }

        public ActionResult SanPhamBonus()
        {
            return PartialView();
        }

        public ActionResult D_SanPham()
        {
            //Tạo viewbag
            List<SanPham> lst1nguoi = db.SanPhams.Where(n => n.MaLoaiSP == 1).ToList();
            List<SanPham> lstnhom = db.SanPhams.Where(n => n.MaLoaiSP == 2).ToList();
            List<SanPham> lstGa_quay = db.SanPhams.Where(n => n.MaLoaiSP == 3).ToList();
            List<SanPham> lstCom = db.SanPhams.Where(n => n.MaLoaiSP == 4).ToList();
            List<SanPham> lstNhe = db.SanPhams.Where(n => n.MaLoaiSP >= 5).ToList();



            ViewBag.lst1nguoi = lst1nguoi;
            ViewBag.lstnhom = lstnhom;
            ViewBag.lstGa_quay = lstGa_quay;
            ViewBag.lstCom = lstCom;
            ViewBag.lstNhe = lstNhe;



            return View();

        }

        //-------------------Detail--------------//
        public ActionResult Detail(int id)
        {
            var D_SanPham = db.SanPhams.Where(s => s.MaSP == id).First();
            return View(D_SanPham);
        }
       

    }
}