using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL;
using BTL.Models;
using PagedList;


namespace Web_BTL.Areas.Admin.Controllers
{
    public class HoaDonController : Controller
    {
        private CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
        public ActionResult QuanLyHoaDonBan()
        {
            return View();
        }

        public ViewResult HoaDonTheoLoai(int? page , int MaHoaDonBan = 9)
        {
            int pagesize = 6;
            int pagenumber = (page ?? 1);


            HOADONBAN lst_hoaDon = db.HOADONBANs.SingleOrDefault(n => n.MaHoaDonBan == MaHoaDonBan);
            if (lst_hoaDon == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<CT_HOADONBAN> lstCTHoaDon = db.CT_HOADONBAN.Where(n => n.MaHoaDonBan == MaHoaDonBan).OrderBy(n => n.MaHoaDonBan).ToList();
            if (lstCTHoaDon.Count == 0)
            {
                ViewBag.lstSanPham = "Không có sản phẩm nào";
            }
            ViewBag.MaHoaDon = MaHoaDonBan;
            ViewBag.lstSanPham = db.CT_HOADONBAN.ToList();
            return View(lstCTHoaDon.ToPagedList(pagenumber, pagesize));
        }
    }
}