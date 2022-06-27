using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL;
using BTL.Models;
using Web_BTL.Areas.Admin.Models;
using Web_BTL.Areas.User.Models;

namespace Web_BTL.Areas.Admin.Controllers
{
    public class GioHangController : Controller
    {
        private CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
        // GET: Admin/GioHang
        public ActionResult Index()
        {
            return View();
        }

        public List<GioHang> LayGioHang()
        {

            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //nếu giỏ hàng chưa tồn tại thì tiến hành khởi tạo list giỏ hàng(session GioHang)
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //thêm giỏ hàng
        public ActionResult ThemGioHang(int sMaSP, string strURL)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sMaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra sản phẩm này đã tồn tại trong session[GioHang] hay chưa...
            GioHang gh = lstGioHang.Find(n => n.sMaSP == sMaSP);
            if (gh == null)
            {
                gh = new GioHang(sMaSP);
                //Add sản phẩm vào list
                lstGioHang.Add(gh);
                return Redirect(strURL);
            }
            else
            {
                gh.iSoLuong++;
                return Redirect(strURL);
            }
        }
        //sửa giỏ hàng
        public ActionResult CapNhatGioHang(int sMaSP, FormCollection f)
        {
            //Kiểm tra mã sản phẩm 
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sMaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();

            GioHang gh = lstGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            //Nếu tồn tại thì cho phép sửa số lượng
            if (gh != null)
            {
                gh.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int sMaSP)
        {
            //Kiểm tra mã sản phẩm 
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sMaSP);
            //nếu get sai mã sản phẩm thì trả về trang lỗi
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra sản phẩm có tồn tại trong session[GioHang]
            GioHang gh = lstGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            //Nếu tồn tại thì cho phép xóa 
            if (gh != null)
            {
                lstGioHang.RemoveAll(n => n.sMaSP == sMaSP);

            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        //xây dựng trang giỏ hàng	
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstgiohang = LayGioHang();
            return View(lstgiohang);
        }
        //tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //tính tổng tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);

            }
            return dTongTien;
        }
        //tạo partial giỏ hàng
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstgiohang = LayGioHang();
            return View(lstgiohang);
        }
    }
}