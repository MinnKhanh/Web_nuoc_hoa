using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BTL;
using BTL.Models;

namespace Web_BTL.Areas.User.Models
{
    public class GioHang
    {
        CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
        public int sMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sAnhSP { get; set; }
        public double dGia { get; set; }
        public int iSoLuong { get; set; }
        public int soluongton { get; set; }
        public double dThanhTien
        {
            get { return dGia * iSoLuong; }
        }
        public GioHang(int MaSP)
        {
            sMaSP = MaSP;
            SANPHAM sanpham = db.SANPHAMs.SingleOrDefault(n => n.MaSP == sMaSP);
            sTenSP = sanpham.TenSP;
            sAnhSP = sanpham.AnhSanPham;
            dGia = double.Parse(sanpham.DonGia.ToString());
            iSoLuong = 1;
            soluongton =(int)sanpham.SoLuong;
        }
    }
}