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
    public class TimKiemController : Controller
    {
        CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
        [HttpPost]
        public ActionResult Searching(FormCollection f, int? page)
        {
            string searchkey = f["txtKeyWord"].ToString();
            ViewBag.keyword = searchkey;
            List<SANPHAM> lstKQ = db.SANPHAMs.Where(n => n.TenSP.Contains(searchkey)).ToList();
            int pagenumber = (page ?? 1);
            int pagesize = 6;
            if (lstKQ.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào, bạn có thể tham khảo các sản phẩm dưới đây";
                return View(db.SANPHAMs.OrderBy(n => n.TenSP).ToPagedList(pagenumber, pagesize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstKQ.Count + " kết quả";
            return View(lstKQ.OrderBy(n => n.TenSP).ToPagedList(pagenumber, pagesize));
        }
        [HttpGet]
        public ActionResult Searching(int? page, string searchkey)
        {
            ViewBag.keyword = searchkey;
            List<SANPHAM> lstKQ = db.SANPHAMs.Where(n => n.TenSP.Contains(searchkey)).ToList();
            int pagenumber = (page ?? 1);
            int pagesize = 12;
            if (lstKQ.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(db.SANPHAMs.OrderBy(n => n.TenSP).ToPagedList(pagenumber, pagesize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstKQ.Count + " kết quả";
            return View(lstKQ.OrderBy(n => n.TenSP).ToPagedList(pagenumber, pagesize));
        }
    }
}