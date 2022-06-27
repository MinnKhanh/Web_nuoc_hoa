using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL;
using BTL.Models;

namespace Web_BTL.Areas.User.Controllers
{
    public class CTDonHangController : Controller
    {
        private CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
        public ActionResult QuanLyHoaDon()
        {//db.CT_HOADONBAN.ToList()

            return View();
        }
    }
}