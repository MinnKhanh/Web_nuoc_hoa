using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL;
using BTL.Models;
using PagedList;

namespace Web_BTL.Areas.User.Controllers
{
    public class SanPhamUserController : Controller
    {
        CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();

        public ActionResult Index(int? page)
        {
           
            return View();
        }

        public PartialViewResult ChuDePartial()
        {
            return PartialView(db.LOAIs.ToList());
        }

       
        public ViewResult SanPhamTheoLoai(int? page, int MaLoai = 1)
        {
            int pagesize = 6; // so san pham tren  1 trang
            int pagenumber = (page ?? 1);


            LOAI lsp = db.LOAIs.SingleOrDefault(n => n.MaLoai == MaLoai);
            if (lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<SANPHAM> lstSanPham = db.SANPHAMs.Where(n => n.MaLoai == MaLoai).OrderBy(n => n.TenSP).ToList();
            if (lstSanPham.Count == 0)
            {
                ViewBag.lstSanPham = "No Result";
            }
            ViewBag.MaLoai = MaLoai;
            ViewBag.lstSanPham = db.SANPHAMs.ToList();
            return View(lstSanPham.ToPagedList(pagenumber, pagesize));
        }

        public ViewResult ChiTietSanPham(int MaSp)
        {
           
            ViewBag.Masp = MaSp;
            return View();
        }

    }
}