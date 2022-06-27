using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL.Models
{
    public class SanPhamThem
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int DonGia { get; set; }
        public int SoLuong { get; set; }
        public string NongDo { get; set; }
        public string NhomHuong { get; set; }
        public string MoTaSanPham { get; set; }
        public int NamPhatHanhSanPham { get; set; }
        public string AnhSanPham { get; set; }
        public int MaNhanHieu { get; set; }
        public int MaLoai { get; set; }
        public int DungTich { get; set; }
    }
}