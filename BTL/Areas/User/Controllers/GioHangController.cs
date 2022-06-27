using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL;
using BTL.Areas.User.Models;
using BTL.Models;
using Web_BTL.Areas.User.Models;

namespace Web_BTL.Areas.User.Controllers
{
    public class GioHangController : Controller
    {
        private CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
        #region Giỏ hàng
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
       
        [HttpGet]
        public int ThemGioHang(int sMaSP)
        {
            SANPHAM sanpham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sMaSP);
            List<GioHang> lstGioHang = LayGioHang();
            if (sanpham == null)
            {
                int count = lstGioHang.Sum(n => n.iSoLuong);
                return count;

            }
            SANPHAM sp = db.SANPHAMs.Single(n => n.MaSP == sMaSP);
            GioHang spgh = lstGioHang.Find(n => n.sMaSP == sMaSP);
            int soluong = (int)sp.SoLuong;
           if((int)sp.SoLuong == 0)
            {
                int count = lstGioHang.Sum(n => n.iSoLuong);
                return count;
            }
            else if (spgh == null && (int)sp.SoLuong>0)
            {
                spgh = new GioHang(sMaSP);
                lstGioHang.Add(spgh);
                int count = lstGioHang.Sum(n => n.iSoLuong);
                return count;
            }
            else
            {
                int sotrongcart = spgh.iSoLuong;
                if (sp.SoLuong<= (sotrongcart++))
                {
                    int count = lstGioHang.Sum(n => n.iSoLuong);
                    return count;
                }
                else
                {
                spgh.iSoLuong++;
                    int count = lstGioHang.Sum(n => n.iSoLuong);
                    return count;
                }
                
            }
           
        }

        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "SanPhamUser");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }

       
        [HttpGet]
        public int TongSoLuong()
        {

            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang != null)
            {
                int iTongSL = 0;
                iTongSL = lstGioHang.Sum(n => n.iSoLuong);
                return iTongSL;
            }
            return 0;
           
        }

       
        [HttpGet]
        public double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
                return dTongTien;
            }
            return dTongTien;
        }
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() != 0)
            {
                ViewBag.TongSoLuong = TongSoLuong();
            }
            return PartialView();
        }
        public ActionResult SuaGioHang()
        {

            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "SanPhamUser");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien(); 
            return View(lstGioHang);
        }
        [HttpGet]
        public List<GioHang> GioHangKhachHang()
        {
            if (Session["GioHang"] == null)
            {
                return null;
            }
            else
            {
                List<GioHang> lstGioHang = LayGioHang();

                return lstGioHang;
            }
        }
       
        [HttpGet]
        public int CapNhatGioHang(int sMaSP, int soluongcapnhat)
        {
            SANPHAM sanpham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sMaSP);
            if (sanpham == null)
            {
                return 0;
            }
            List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra sản phẩm có trong giỏ hàng không
            GioHang sp = lstGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (sp != null)
            {
                sp.iSoLuong = soluongcapnhat;

            }
            return 1;
        }
       
        [HttpGet]
        public int XoaGioHang(int sMaSP)
        {
            SANPHAM sanpham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sMaSP);
            if (sanpham == null)
            {
                return 0;
            }
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.RemoveAll(n => n.sMaSP == sMaSP);
            return 1;
        }

        #endregion
      
        public ActionResult DatHang()
        {
            //kiểm tra đăng nhập
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "TaiKhoanUser");
            }
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "SanPhamUser");
            }
            //lưu thông tin vào bảng hóa đơn
            HOADONBAN hdb = new HOADONBAN();
            hdb.NgayBan = DateTime.Now;
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            hdb.MaKhachHang = kh.MaKhachHang;
            hdb.TongTienBan = (int)TongTien();
            db.HOADONBANs.Add(hdb);
            db.SaveChanges();
            // lưu thông tin vào chi tiết hóa đơn
            List<GioHang> lstGioHang = LayGioHang();
            foreach (var item in lstGioHang)
                {
                    CT_HOADONBAN cthd = new CT_HOADONBAN();
                    cthd.MaHoaDonBan = hdb.MaHoaDonBan;
                    cthd.MaSP = item.sMaSP;
                    cthd.SoLuongBan = item.iSoLuong;
                    cthd.DonGiaBan = int.Parse(item.dGia.ToString());
                    cthd.ThanhTienBan = int.Parse(item.dThanhTien.ToString());
                    db.CT_HOADONBAN.Add(cthd);
                }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("Index","SanPhamUser");
        }
    }
}