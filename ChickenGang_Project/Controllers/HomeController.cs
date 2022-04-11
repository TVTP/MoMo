using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ChickenGang_Project.Models;
using PagedList;


namespace ChickenGang_Project.Controllers
{
    public class HomeController : Controller
    {
        dbChickenGang db = new dbChickenGang();

        
        public ActionResult Index()
        {
            //Tạo viewbag
            List<SanPham> lst1nguoi = db.SanPhams.Where(n => n.MaLoaiSP == 1).ToList();
            List<SanPham> lstnhom = db.SanPhams.Where(n => n.MaLoaiSP == 2).ToList();
            List<SanPham> lstle = db.SanPhams.Where(n => n.MaLoaiSP > 3).ToList();
            List<SanPham> lstbonus = db.SanPhams.Where(n => n.MaLoaiSP == 6).ToList();

            List<SanPham> lstall = db.SanPhams.ToList();


            ViewBag.lstall = lstall;
            ViewBag.lst1nguoi = lst1nguoi;
            ViewBag.lstnhom = lstnhom;
            ViewBag.lstle = lstle;
            ViewBag.lstle = lstbonus;


            return View();
        }

        //public ActionResult Index2 (int? page)
        //{
        //    if (page == null) page = 1;
        //    var lstall = db.SanPhams.ToList();

        //    int pageSize = 6;
        //    int pageNum = page ?? 1;

        //    return View(lstall.ToPagedList(pageNum,pageSize));
        //}

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            ////Kiem tra UserName & Password
            //string sTaiKhoan = f["uname"].ToString();
            //string sMatKhau = f["psw"].ToString();

            //ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            //if (tv != null)
            //{
            //    Session["TaiKhoan"] = tv;
            //    //return Content("<script>window.location.reload();</script>");
            //    return View("Index");

            //}

            //return Content("Tài khoản hoặc mật khẩu không đúng!!!");

            string taikhoan = f["uname"].ToString();
            string matkhau = f["psw"].ToString();
            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == taikhoan && n.MatKhau == matkhau);
            if(tv != null)
            {
               //var lstQuyen = db.LoaiThanhVien_Quyens.Where(n => n.MaLoaiTV == tv.MaLoaiTV);
               // string Quyen = "";
               // foreach( var item in lstQuyen)
               // {
               //     Quyen += item.Quyen.MaQuyen + ",";
               // }
               // Quyen = Quyen.Substring(0, Quyen.Length - 1);
               // PhanQuyen(tv.TaiKhoan.ToString(), Quyen);
                return Content("<script>window.location.reload();</script>");
            }
            return Content("Tài khoản hoặc mật khẩu không đúng!!!");
        }
        public void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();

            var ticket = new FormsAuthenticationTicket(1,
                                                TaiKhoan,
                                                DateTime.Now,
                                                DateTime.Now.AddHours(3),
                                                false,
                                                Quyen,
                                                FormsAuthentication.FormsCookiePath);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));

            if(ticket.IsPersistent) cookie.Expires = ticket.Expiration;

            Response.Cookies.Add(cookie);
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}