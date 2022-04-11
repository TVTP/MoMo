using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChickenGang_Project.Models;

namespace ChickenGang_Project.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        dbChickenGang db = new dbChickenGang();
        public ActionResult Index()
        {
            var sp = db.SanPhams.ToList();
            return View(sp);
        }
       
        public ActionResult Detail(int id)
        {
            var D_SanPham = db.SanPhams.Where(s => s.MaSP == id).First();
            return View(D_SanPham);
        }

        public ActionResult Delete(int id)
        {
            var D_sach = db.SanPhams.First(m => m.MaSP == id);
            return View(D_sach);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_sach = db.SanPhams.Where(m => m.MaSP == id).First();
            db.SanPhams.Remove(D_sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var E_sach = db.SanPhams.First(m => m.MaSP == id);
            return View(E_sach);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_sp = db.SanPhams.First(m => m.MaSP == id);
            var E_tensach = collection["TenSP"];
            var E_hinh = collection["HinhAnh"];
            var E_dongia = collection["DonGia"];
            var E_soluongton = collection["SoLuongTon"];
            var E_mota = collection["MoTa"];
            var E_mota1 = collection["MoTa1"];
            var E_mota2 = collection["MoTa2"];

            E_sp.MaSP = id;
            if (string.IsNullOrEmpty(E_tensach))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_sp.TenSP = E_tensach;
                E_sp.HinhAnh = E_hinh;
                E_sp.DonGia = Convert.ToDecimal(E_dongia);
                E_sp.SoLuongTon = Convert.ToInt32(E_soluongton);
                E_sp.Mota = E_mota;
                E_sp.Mota2 = E_mota1;
                E_sp.Mota3 = E_mota2;
                UpdateModel(E_sp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }


        public ActionResult Create()
        {
            dbChickenGang context = new dbChickenGang();
            SanPham objSP = new SanPham();

            objSP.ListNCC = context.NhaCungCaps.ToList();
            objSP.ListNSX = context.NhaSanXuats.ToList();
            objSP.ListLSP = context.LoaiSanPhams.ToList();

            return View(objSP);
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, SanPham s)
        {
            var E_tensach = collection["TenSP"];
            var E_hinh = collection["HinhAnh"];
            var E_dongia = collection["DonGia"];
            var E_soluongton = collection["SoLuongTon"];
            var E_mota = collection["MoTa"];
            var E_mota1 = collection["MoTa2"];
            var E_mota2 = collection["MoTa3"];



            var E_mancc = collection["NhaCungCap"];
            var E_mansx = collection["NhaSanXuat"];
            var E_maloaisp = collection["LoaiSanPham"];
            if (string.IsNullOrEmpty(E_tensach))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.TenSP = E_tensach.ToString();
                s.HinhAnh = E_hinh.ToString();
                s.DonGia = Convert.ToDecimal(E_dongia);
                s.SoLuongTon = Convert.ToInt32(E_soluongton);
                s.MaNCC = Convert.ToInt32(E_mancc);
                s.MaNSX = Convert.ToInt32(E_mansx);
                s.MaLoaiSP = Convert.ToInt32(E_maloaisp);
                s.NgayCapNhat = DateTime.Now;
                s.Mota = E_mota;
                s.Mota2 = E_mota1;
                s.Mota3 = E_mota2;
                s.Moi = 1;
                s.LuotBinhChon = 0;
                s.LuotXem = 0;
                db.SanPhams.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        //Khách hàng
        #region KhachHang
        public ActionResult thanhvien()
        {
            var sp = db.ThanhViens.ToList();
            return View(sp);
        }

        public ActionResult Delete_ThanhVien(int id)
        {
            var D_ThanhVien = db.ThanhViens.First(m => m.MaThanhVien == id);
            return View(D_ThanhVien);
        }
        [HttpPost]
        public ActionResult Delete_ThanhVien(int id, FormCollection collection)
        {
            var D_ThanhVien = db.ThanhViens.Where(m => m.MaThanhVien == id).First();
            db.ThanhViens.Remove(D_ThanhVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


       

        public ActionResult Edit_ThanhVien(int id)
        {
            var E_ThanhVien = db.ThanhViens.First(m => m.MaThanhVien == id);
            return View(E_ThanhVien);
        }
        [HttpPost]
        public ActionResult Edit_ThanhVien(int id, FormCollection collection)
        {
            var E_ThanhVien = db.ThanhViens.First(m => m.MaThanhVien == id);
            var E_HoTen = collection["HoTen"];
            var E_MatKhau = collection["MatKhau"];
            var E_DiaChi = collection["DiaChi"];
            var Email = collection["Email"];
            var E_SoDienThoai = collection["SoDienThoai"];


            E_ThanhVien.MaThanhVien = id;
            if (string.IsNullOrEmpty(E_HoTen))
            {
                

                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_ThanhVien.TaiKhoan = E_ThanhVien.TaiKhoan;
                E_ThanhVien.MatKhau = E_MatKhau;
                E_ThanhVien.HoTen = E_HoTen;
                E_ThanhVien.DiaChi = E_DiaChi;
                E_ThanhVien.Email = Email;
                E_ThanhVien.SoDienThoai = E_SoDienThoai;
                E_ThanhVien.CauHoi = E_ThanhVien.CauHoi;
                E_ThanhVien.CauTraLoi = E_ThanhVien.CauTraLoi;
                E_ThanhVien.MaLoaiTV = E_ThanhVien.MaLoaiTV;
                E_ThanhVien.LoaiAccount = E_ThanhVien.LoaiAccount;
                UpdateModel(E_ThanhVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
            }
        #endregion
    }
}