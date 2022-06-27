using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_BTL.Areas.Admin.Models
{
    public class SanPhamViewModel
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public Nullable<int> DonGia { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public string NongDo { get; set; }
        public string NhomHuong { get; set; }
        public string MoTaSanPham { get; set; }
        public Nullable<int> NamPhatHanhSanPham { get; set; }
        public string AnhSanPham { get; set; }
        public Nullable<int> MaNhanHieu { get; set; }
        public Nullable<int> MaLoai { get; set; }
        public Nullable<int> DungTich { get; set; }
        public string TenNhanHieu { get; set; }
        public string TenLoai { get; set; }
    }
}