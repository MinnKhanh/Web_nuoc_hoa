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
    public class KhachHangController : Controller
    {
        private CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
        // GET: Admin/KhachHang
        public ActionResult QuanLyKhachHang()
        {
            return View(db.KHACHHANGs.ToList());
        }

        // Xóa khách hàng:
        [HttpGet]
        public ActionResult XoaKhachHang(int MaKhachHang)
        {
            KHACHHANG khachhang = db.KHACHHANGs.Single(n => n.MaKhachHang == MaKhachHang);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }

        [HttpPost, ActionName("XoaKhachHang")]
        public ActionResult XacNhanXoa(int MaKhachHang)
        {
            KHACHHANG khachhang = db.KHACHHANGs.Single(n => n.MaKhachHang == MaKhachHang);
            var anhsp = from p in db.KHACHHANGs
                        where p.MaKhachHang == khachhang.MaKhachHang
                        select p;
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.KHACHHANGs.Remove(khachhang);
            db.SaveChanges();
            return RedirectToAction("QuanLyKhachHang");
        }
    }
}