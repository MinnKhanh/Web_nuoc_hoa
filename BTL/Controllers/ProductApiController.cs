using BTL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BTL.Controllers
{

    public class ProductApiController : ApiController
    {
        CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
        [HttpGet]
        public List<SANPHAM> Index()
        {
           
                return db.SANPHAMs.ToList();
            
            

            
        }

        [Route("SanPhambyCategory")]
        [HttpGet]
        public List<SANPHAM> NhanSanPham(int maloai=0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (maloai == 0)
            {
                List<SANPHAM> lissp = db.SANPHAMs.ToList();
                return lissp;
            }
            else
            {
                List<SANPHAM> lissp = db.SANPHAMs.Where(n => n.MaLoai == maloai).ToList();
                return lissp;
            }
        }
        [Route("Category")]
        [HttpGet]
        public List<LOAI> Category()
        {
            db.Configuration.ProxyCreationEnabled = false;
           return db.LOAIs.ToList();

        }
        [Route("SanPhamPhanTrang")]
        [HttpGet]
        public List<SANPHAM> Phantrng(int page=0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<SANPHAM> list = db.SANPHAMs.OrderBy(n=>n.MaSP).Skip(page).Take(1).ToList();
            return list;

        }
        [Route("gettongtien")]
        [HttpGet]
        public CapNhatCart CapNhatGioHang(int sMaSP = 1, int soluongcapnhat = 0)
        {
           
            
            CapNhatCart capnhat = new CapNhatCart(2, 5);
            return capnhat;
        }
        [Route("Search")]
        [HttpGet]
        public List<SANPHAM> SearchSanPham(string searchkey = "")
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (searchkey == "")
            {
                return db.SANPHAMs.ToList();
            }
           else
           {

                return db.SANPHAMs.Where(n => n.TenSP.Contains(searchkey)).ToList();
            }

            
        }

        [Route("HoaDon")]
        [HttpGet]
        public IEnumerable HoaDon(int makhachhang)
        {
            db.Configuration.ProxyCreationEnabled = false;
            

                return db.getHoadon(makhachhang).ToList();

        }
        [Route("ChiTietHoaDonBan")]
        [HttpGet]
        public IEnumerable ChiTietHoaDon(int mahoadon)
        {
            db.Configuration.ProxyCreationEnabled = false;


            return db.getCTHoadon(mahoadon).ToList();

        }
        [Route("ChiTietSanPham")]
        [HttpGet]
        public SANPHAM ChiTietSanPham(int MaSP)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SANPHAMs.Where(n=>n.MaSP==MaSP).SingleOrDefault();

        }
    }
}
