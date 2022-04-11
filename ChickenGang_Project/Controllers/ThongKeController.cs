using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChickenGang_Project.Models;

namespace ChickenGang_Project.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: ThongKe
        dbChickenGang db = new dbChickenGang();
        public ActionResult Index()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();
            ViewBag.SoNguoiDangOnline = HttpContext.Application["SoNguoiDangOnline"].ToString();
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu();
            ViewBag.TongDonHang = ThongKeDonHang();
            ViewBag.TongThanhVien = ThongKeThanhVien();

            var sp = db.DonDatHangs.ToList();
            return View(sp);
        }
        public decimal ThongKeTongDoanhThu()
        {
            var lstDDH = db.DonDatHangs.ToList();
            decimal TongTien = 0;
            foreach (var item in lstDDH)
            {
                TongTien += decimal.Parse(item.ChiTietDonDat.Sum(n => n.SoLuong * n.DonGia).Value.ToString());
            }
            return TongTien;
        }
        public decimal ThongKeTongDoanhThuThang(int Thang, int Nam)
        {
            var lstDDH = db.DonDatHangs.Where(n => n.NgayDat.Value.Month == Thang && n.NgayDat.Value.Year == Nam);
            decimal TongTien = 0;
            foreach (var item in lstDDH)
            {
                TongTien += decimal.Parse(item.ChiTietDonDat.Sum(n => n.SoLuong * n.DonGia).Value.ToString());
            }
            return TongTien;
        }
        public double ThongKeDonHang()
        {
            double slddh = db.DonDatHangs.Count();
            return slddh;
        }
        public double ThongKeThanhVien()
        {
            double sltv = db.ThanhViens.Count();
            return sltv;
        }
        public ActionResult ChiTietDonHang(int id)
        {
            ViewBag.TongTien = TongTienDonHang(id);
            var D_thucAn = db.ChiTietDonDats.Where(m => m.MaDDH == id).ToList();
            return View(D_thucAn);
        }
        private double TongTienDonHang(int id)
        {
            var D_thucAn = db.DonDatHangs.Where(m => m.MaDDH == id);
            double tt = 0;
            foreach (var item in D_thucAn)
            {
                tt += double.Parse(item.ChiTietDonDat.Sum(n => n.SoLuong * n.DonGia).Value.ToString());
            }
            return tt;
        }
    }
}