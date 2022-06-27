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
    public class AdminApiController : ApiController
    {
        CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
        [Route("HoaDonAdmin")]
        [HttpGet]
        public IEnumerable HoaDon()
        {
            db.Configuration.ProxyCreationEnabled = false;


            return db.getHoadonAdmin().ToList();

        }
        [Route("TaiKhoanKhachHang")]
        [HttpGet]
        public IEnumerable TaiKhoanKhachHang()
        {
            db.Configuration.ProxyCreationEnabled = false;


            return db.KHACHHANGs.ToList();

        }
        [Route("XoaHoaDon")]
        [HttpDelete]
        public bool XoaHoaDon(int MaHoaDon)
        {
            db.Configuration.ProxyCreationEnabled = false;

            db.removehoadon(MaHoaDon);
            return true;

        }
        [Route("XoaTaiKhoan")]
        [HttpDelete]
        public bool XoaTaiKhoan(int Makh)
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.xoakhachhang(Makh);
            return true;

        }
        [Route("SuaTaiKhoan")]
        [HttpPut]
        public TaiKhoan SuaTaiKhoan(TaiKhoan taikhoan)
        {
            db.Configuration.ProxyCreationEnabled = false;
            
            db.updatetaikhoan((int)taikhoan.MaKhachHang, taikhoan.TenKhachHang, taikhoan.DiaChi, taikhoan.DienThoai,DateTime.Parse(taikhoan.NgaySinh), taikhoan.TenTaiKhoan, taikhoan.MatKhau);
            return taikhoan;

        }
        [Route("TaiKhoanKhachHangTheoMa")]
        [HttpGet]
        public KHACHHANG TaiKhoanKhachHangTheoMa(int Makh)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.KHACHHANGs.Where(n => n.MaKhachHang == Makh).SingleOrDefault();
        }

    }
}
