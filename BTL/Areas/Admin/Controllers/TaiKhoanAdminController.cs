using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL;
using BTL.Models;
using Web_BTL.Areas.User.Models;
namespace Web_BTL.Areas.Admin.Controllers
{
    public class TaiKhoanAdminController : Controller
    {
        private CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
        // GET: User

        // Đăng ký là mặc định tài khoản của khách hàng còn tài khoản của Admin thì chỉ có 1
        
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(KHACHHANG kh)
        {
            if (kh.TenTaiKhoan == "admin" && kh.MatKhau == "admin")
            {
                return RedirectToAction("Index", "SanPhamAdmin");
            }
            ViewBag.DangNhapSai = "Thông tin tài khoản hoặc mật khẩu không chính xác, xin kiểm tra lại! Bạn hãy chắc rằng loại tài khoản của bạn là Admin";
            return View("DangNhap");
        }
    }
}