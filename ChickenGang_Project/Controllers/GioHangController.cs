using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChickenGang_Project.Models;
using Newtonsoft.Json.Linq;

namespace ChickenGang_Project.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        dbChickenGang data = new dbChickenGang();
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult ThemGioHang(int id, string strURL)
        {
            SanPham sp = data.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                //Not Found
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng
            List<GioHang> lstGioHang = Laygiohang();


            //Sản phẩm có trong giỏ hàng
            GioHang spCheck = lstGioHang.SingleOrDefault(n => n.id == id);
            if (spCheck != null)
            {
                //kiểm tra số lượng tồn
                if (sp.SoLuongTon < spCheck.isoluong)
                {
                    Response.Write("<script>alert('Sản phầm đã hết hàng!!')</script>");
                }
                spCheck.isoluong++;
                return Redirect(strURL);
            }
            GioHang itemGioHang = new GioHang(id);
            if (sp.SoLuongTon < itemGioHang.isoluong)
            {
                Response.Write("<script>alert('Sản phầm đã hết hàng!!')</script>");
            }
            lstGioHang.Add(itemGioHang);
            return Redirect(strURL);
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(n => n.isoluong);
            }
            return tsl;
        }
        private int TongSoluongSanPham()
        {
            int tsl = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;

        }

        private double TongTien()
        {
            double tt = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(n => n.dThanhtien);
            }
            return tt;
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoluongSanPham();
            return View(lstGiohang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluon = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoluongSanPham();
            return PartialView();
        }
        public ActionResult XoaGiohang(int id)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.id == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.id == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            SanPham sp = data.SanPhams.SingleOrDefault(n => n.MaSP == id);
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.id == id);
            if (sanpham != null)
            {
                sanpham.isoluong = int.Parse(collection["txtSolg"].ToString());
                if (sp.SoLuongTon < sanpham.isoluong)
                {
                    ViewBag.soluong = "Số lượng sản phẩm không đủ!!";
                    ViewBag.chonlai = "Hãy chọn sản phẩm khác.";
                }
                return View("GioHang"); 
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            //if (Session["TaiKhoan"] == null || Session["Taikhoan"].ToString() == "")
            //{
            //    return RedirectToAction("DangNhap", "NguoiDung");
            //}
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoluongSanPham();
            return View(lstGiohang);
        }

        public ActionResult DatHang(FormCollection collection)
        {
            DonDatHang dh = new DonDatHang();
            ThanhVien kh = (ThanhVien)Session["Taikhoan"];

            if (Session["TaiKhoan"] == null)
            {
                KhachHang kk = new KhachHang();
                kk.TenKH = collection["tenkh"];
                kk.Email = collection["email"];
                var diachi = collection["diachigiao"];
                kk.DiaChi = diachi;
                kk.SoDienThoai = collection["sdt"];
                data.KhachHangs.Add(kk);
                data.SaveChanges();
                SanPham s = new SanPham();
                List<GioHang> gh = Laygiohang();
                var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);

                dh.DiaChiGiaoHang = diachi;
                dh.MaKH = kk.MaKH;
                dh.NgayDat = DateTime.Now;
                dh.NgayGiao = DateTime.Parse(ngaygiao);
                dh.TinhTrangGiaoHang = false;
                dh.DaThanhToan = false;
                var mail = new MailInfo();
                mail.SendEmailOrder(kk.Email, kk.TenKH);
                data.DonDatHangs.Add(dh);
                data.SaveChanges();
                foreach (var item in gh)
                {
                    ChiTietDonDat ctdh = new ChiTietDonDat();
                    ctdh.MaDDH = dh.MaDDH;
                    ctdh.TenSP = item.ten;
                    ctdh.MaSP = item.id;
                    ctdh.SoLuong = item.isoluong;
                    ctdh.DonGia = (decimal)item.giaban;
                    s = data.SanPhams.Single(n => n.MaSP == item.id);
                    s.SoLuongTon -= ctdh.SoLuong;
                    data.SaveChanges();
                    data.ChiTietDonDats.Add(ctdh);

                }
                data.SaveChanges();
                Session["GioHang"] = null;
            }
            else if (Session["TaiKhoan"] != null)
            {
                KhachHang kk = new KhachHang();

                SanPham s = new SanPham();
                List<GioHang> gh = Laygiohang();
                var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
                var diachi = collection["diachigiao"];
                dh.DiaChiGiaoHang = diachi;
                //dh.MaKH = kh.MaThanhVien;
                dh.NgayDat = DateTime.Now;
                dh.NgayGiao = DateTime.Parse(ngaygiao);
                dh.TinhTrangGiaoHang = false;
                dh.DaThanhToan = false;
                kk.TenKH = kh.HoTen;
                kk.Email = kh.Email;
                kk.SoDienThoai = kh.SoDienThoai;
                kk.DiaChi = kh.DiaChi;
                var mail = new MailInfo();
                mail.SendEmailOrder(kh.Email, kh.HoTen);
                data.KhachHangs.Add(kk);
                data.DonDatHangs.Add(dh);
                data.SaveChanges();
                foreach (var item in gh)
                {
                    ChiTietDonDat ctdh = new ChiTietDonDat();
                    ctdh.MaDDH = dh.MaDDH;
                    ctdh.MaSP = item.id;
                    ctdh.SoLuong = item.isoluong;
                    ctdh.DonGia = (decimal)item.giaban;
                    s = data.SanPhams.Single(n => n.MaSP == item.id);
                    s.SoLuongTon -= ctdh.SoLuong;
                    data.SaveChanges();
                    data.ChiTietDonDats.Add(ctdh);

                }
                data.SaveChanges();
                Session["GioHang"] = null;
            }


            return RedirectToAction("XacnhanDonhang", "GioHang");
        }
        public ActionResult ReturnUrl()
        {
            string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = Server.UrlDecode(param);
            MoMoSecurity crypto = new MoMoSecurity();
            string serectkey = "5LbRBNFYIJicZvWAJC84wGIJC0WUZD2Y";
            string signature = crypto.signSHA256(param, serectkey);
            if (signature != Request["signature"].ToString())
            {
                ViewBag.message = "Thông tin Request không hợp lệ";
                return View();
            }
            if (!Request.QueryString["errorCode"].Equals("0"))
            {
                ViewBag.message = "Thanh toán thất bại";
            }
            else
            {
                ViewBag.message = "Thanh toán thành công";
                Session["GioHang"] = new List<GioHang>();
            }
            return View();
        }

        public JsonResult NotifyUrl()
        {

            string param = "";
            param = "partner_code=" + Request["partner_code"] +
                    "&access_key=" + Request["access_key"] +
                    "&amount=" + Request["amount"] +
                    "&order_id=" + Request["ordert_id"] +
                    "&order_info=" + Request["order_info"] +
                    "&order_type=" + Request["order_type"] +
                    "&transaction_id=" + Request["transaction_id"] +
                    "&message=" + Request["message"] +
                    "&response_time=" + Request["response_time"] +
                    "&status_code=" + Request["status_code"];
            param = Server.UrlDecode(param);

            MoMoSecurity crypto = new MoMoSecurity();
            string serectkey = "5LbRBNFYIJicZvWAJC84wGIJC0WUZD2Y";
            string signature = crypto.signSHA256(param, serectkey);


            if (signature != Request["signature"].ToString())
            {

            }

            string status_code = Request["status_code"].ToString();
            if ((status_code != "0"))
            {

            }
            else
            {
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Payment()
        {
            //request params need to request to MoMo system
            List<GioHang> gioHang = Session["GioHang"] as List<GioHang>;
            string endpoint = "https://payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMO5H8J20220410";
            string accessKey = "xDw8Rm6Tq90aZY3D";
            string serectkey = "5LbRBNFYIJicZvWAJC84wGIJC0WUZD2Y";
            string orderInfo = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string returnUrl = "https://localhost:44325/GioHang/ReturnUrl";
            string notifyurl = "https://51b6-113-173-228-230.ngrok.io/GioHang/NotifyUrl"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = gioHang.Sum(n => n.dThanhtien).ToString();
            string orderid = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }
        public ActionResult xacnhandonhang()
        {
            return View();
        }
    }
}