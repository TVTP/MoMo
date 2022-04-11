using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChickenGang_Project.Models;
using CaptchaMvc;
using CaptchaMvc.HtmlHelpers;
using System.Text;
using System.Security.Cryptography;

namespace ChickenGang_Project.Controllers
{
    public class NguoiDungController : Controller
    {
        dbChickenGang db = new dbChickenGang();

        // GET: NguoiDung



        //Dang Ky
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangky(FormCollection f, ThanhVien tv)
        {

            var HoTen = f["HoTen"];
            var TaiKhoan = f["TaiKhoan"];
            var MatKhau = f["MatKhau"];
            var XacNhanMatKhau = f["XacNhanMatKhau"];
            var DiaChi = f["DiaChi"];
            var Email = f["Email"];
            var SoDienThoai = f["SoDienThoai"];
            var CauHoi = f["CauHoi"];
            var CauTraLoi = f["CauTraLoi"];


            // Code for validating the CAPTCHA  
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                var check = db.ThanhViens.FirstOrDefault(s => s.Email == tv.Email);
                if(check != null)
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
                else if (String.IsNullOrEmpty(XacNhanMatKhau))
                {
                    ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận!";
                }

                else
                {
                    if (!MatKhau.Equals(XacNhanMatKhau))
                    {
                        ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";
                    }
                    else
                    {
                        tv.HoTen = HoTen;
                        tv.TaiKhoan = TaiKhoan;
                        tv.MatKhau = GetMD5(MatKhau).ToString();
                        tv.DiaChi = DiaChi;
                        tv.Email = Email;
                        tv.SoDienThoai = SoDienThoai;
                        tv.CauHoi = CauHoi;
                        tv.CauTraLoi = CauTraLoi;
                        tv.MaLoaiTV = 1;
                        var mail = new MailInfo();
                        mail.SendEmail(Email,HoTen);
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.ThanhViens.Add(tv);
                        db.SaveChanges();
                        ViewBag.ThongBao = "Thêm thành công";
                        //return RedirectToAction("DangNhap");
                        return View();

                    }
                }

            }
            ViewBag.ThongBao = "Sai mã Captcha";

            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        //Dang Nhap
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            //Kiem tra UserName & Password
            string sTaiKhoan = f["uname"];
            string sMatKhau = f["psw"];

            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
          
            //Account ac = db.Accounts.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if ( tv != null)
            {
               
                if(tv.LoaiAccount== "1")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    Session["TaiKhoan"] = tv;
                    return RedirectToAction("Index", "Home");
                }



            }


            return Content("Tài khoản hoặc mật khẩu không đúng!!!");
        }


        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public ActionResult dangxuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var nd = db.ThanhViens.First(m => m.MaThanhVien == id); 
            return View(nd);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_sp = db.ThanhViens.First(m => m.MaThanhVien == id);
            var E_ten = collection["HoTen"];
            var E_taikhoan = collection["TaiKhoan"];
            var E_matkhau = collection["MatKhau"];
            var E_diachi = collection["DiaChi"];
            var E_email = collection["Email"];
            var E_sdt = collection["SoDienThoai"];

            E_sp.MaThanhVien = id;
            if (string.IsNullOrEmpty(E_ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_sp.HoTen = E_ten;
                E_sp.DiaChi = E_diachi;
                E_sp.Email = E_email;
                E_sp.SoDienThoai = E_sdt;
                UpdateModel(E_sp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
      
    }
}