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
    public class NhanHieuController : Controller
    {
        CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
        // GET: Admin/SanPhamAdmin

        public ActionResult QuanLyNhanHieu()
        {
            return View();
        }
        // Thêm sản phẩm:
        [HttpGet]
        public ActionResult ThemNhanHieu()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemNhanHieu([Bind(Include = " MaNhanHieu , TenNhanHieu , Email , XuatXu ")] NHANHIEU nhanhieu)
        {

            if (ModelState.IsValid)
            {
                db.NHANHIEUx.Add(nhanhieu);
                db.SaveChanges();
                return RedirectToActionPermanent("Index","SanPhamAdmin");
            }
            return View(nhanhieu);
        }

        // Chỉnh sửa sản phẩm:
        public ActionResult ChinhSuaNhanHieu(int MaNhanHieu)
        {
            if (MaNhanHieu == null)
            {
                return HttpNotFound();
            }
            NHANHIEU nhanhieu = db.NHANHIEUx.Find(MaNhanHieu);
            if (nhanhieu == null)
            {
                return HttpNotFound();
            }

            return View(nhanhieu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChinhSuaNhanHieu([Bind(Include = " MaNhanHieu , TenNhanHieu , Email , XuatXu ")] NHANHIEU nhanhieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanhieu).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("QuanLyNhanHieu");
            }
            return RedirectToAction("QuanLyNhanHieu");
        }

        // Xóa sản phẩm:
        [HttpGet]
        public ActionResult XoaNhanHieu(int MaNhanHieu)
        {
            NHANHIEU nhanhieu = db.NHANHIEUx.Single(n => n.MaNhanHieu == MaNhanHieu);
            if (nhanhieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhanhieu);
        }

        [HttpPost, ActionName("XoaNhanHieu")]
        public ActionResult XacNhanXoa(int MaNhanHieu)
        {
            NHANHIEU nhanhieu = db.NHANHIEUx.Single(n => n.MaNhanHieu == MaNhanHieu);

            if (nhanhieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NHANHIEUx.Remove(nhanhieu);
            db.SaveChanges();
            return RedirectToAction("QuanLyNhanHieu");
        }
    }
}