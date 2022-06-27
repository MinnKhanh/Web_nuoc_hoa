using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL;
using BTL.Models;
using Web_BTL.Areas.User.Models;

namespace Web_BTL.Areas.User.Controllers
{
    public class TaiKhoanUserController : Controller
    {
        private CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
        // GET: User

        // Đăng ký là mặc định tài khoản của khách hàng còn tài khoản của Admin thì chỉ có 1
        public ActionResult DangKy()
        {
            if (Session["MaKhachHang"] != null)
            {
                return RedirectToAction("Index", "SanPhamUser");
            }
            if (Session["Admin"] != null)
            {
                return RedirectToAction("Index", "Admin");
            }
          //  KHACHHANG user = new KHACHHANG();user
            return View();
        }

        [HttpPost]

        public ActionResult DangKy(KHACHHANG kh)
        {
            // Kiểm tra nếu tên đăng nhập đã tồn tại:
            if (db.KHACHHANGs.Any(n => n.TenTaiKhoan == kh.TenTaiKhoan))
            {
                ViewBag.TrungTaiKhoan = "Tên tài khoản đã tồn tại , xin thử tên khác";
                return View("DangKy", kh);
            }
            else
            {
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.ThanhCong = "Đăng ký tài khoản thành công";
            }

           return RedirectToAction("DangNhap", "TaiKhoanUser");
        }
        public ActionResult DangXuat()
        {
            Session["MaKhachHang"] = null;
            Session["TenDangNhap"] = null;
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index", "SanPhamUser");
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(KHACHHANG kh , FormCollection account)
        {
            string TaiKhoan = account["TenTaiKhoan"].ToString();
            string PassWord = account["MatKhau"].ToString();
            kh = db.KHACHHANGs.SingleOrDefault(x => x.TenTaiKhoan.Equals(TaiKhoan) && x.MatKhau.Equals(PassWord));
            if (kh != null)
            {
               
                Session["MaKhachHang"] = kh.MaKhachHang;
                Session["TenDangNhap"] = kh.TenKhachHang;
                Session["TaiKhoan"] = kh;
                return RedirectToAction("Index", "SanPhamUser");
            //}
            }
            ViewBag.DangNhapSai = "Thông tin tài khoản hoặc mật khẩu không chính xác, xin kiểm tra lại! Bạn hãy chắc rằng loại tài khoản của bạn đang sử dụng là tài khoản dành cho khách hàng";
            return View("DangNhap");
        }

    }
}
