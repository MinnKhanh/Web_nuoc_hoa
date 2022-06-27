using BTL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BTL.Controllers
{
    public class SPController : ApiController
    {
        [HttpGet]
        public List<SANPHAM> GetCustomerLists()
        {
            CSDL_QuanLyCuaHangBanNuocHoa dbSANPHAM = new CSDL_QuanLyCuaHangBanNuocHoa();
            List<SANPHAM> SANPHAMlist = new List<SANPHAM>();
            var query = (from prods in dbSANPHAM.SANPHAMs select prods).ToList();
            foreach (var item in query)
            {
                SANPHAMlist.Add(new SANPHAM
                {
                    MaSP = item.MaSP,
                    TenSP = item.TenSP,
                    DonGia = item.DonGia,
                    SoLuong = item.SoLuong,
                    NongDo = item.NongDo,
                    NhomHuong = item.NhomHuong,
                    MoTaSanPham = item.MoTaSanPham,
                    NamPhatHanhSanPham = item.NamPhatHanhSanPham,
                    AnhSanPham = item.AnhSanPham,
                    MaNhanHieu = item.MaNhanHieu,
                    MaLoai = item.MaLoai,
                    DungTich = item.DungTich,
                });
            }
            return SANPHAMlist;

        }




        [HttpPost]
        public bool InsertNewSANPHAM(SanPhamThem spadd)
        {
            CSDL_QuanLyCuaHangBanNuocHoa dbSANPHAM = new
                CSDL_QuanLyCuaHangBanNuocHoa();
            dbSANPHAM.Configuration.ProxyCreationEnabled = false;
            try
            {
                SANPHAM customer = new SANPHAM();
                customer.TenSP = spadd.TenSP;
                customer.DonGia = spadd.DonGia;
                customer.SoLuong = spadd.SoLuong;
                customer.NongDo = spadd.NongDo;
                customer.NhomHuong = spadd.NhomHuong;
                customer.MoTaSanPham = spadd.MoTaSanPham;
                customer.NamPhatHanhSanPham = spadd.NamPhatHanhSanPham;
                customer.AnhSanPham = spadd.AnhSanPham;
                customer.MaNhanHieu = spadd.MaNhanHieu;
                customer.MaLoai = spadd.MaLoai;
                customer.DungTich = spadd.DungTich;
                dbSANPHAM.SANPHAMs.Add(customer);
                dbSANPHAM.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpGet]
        public SANPHAM GetSANPHAM(int id)
        {
            CSDL_QuanLyCuaHangBanNuocHoa dbSANPHAM = new CSDL_QuanLyCuaHangBanNuocHoa();
            dbSANPHAM.Configuration.ProxyCreationEnabled = false;
            return dbSANPHAM.SANPHAMs.FirstOrDefault(x => x.MaSP == id);
        }
        [Route("UpdateSP")]
        [HttpPut]
        public string Put(SanPhamThem av)
        {
            CSDL_QuanLyCuaHangBanNuocHoa dbSANPHAM = new
                CSDL_QuanLyCuaHangBanNuocHoa();
            var Av_ = dbSANPHAM.SANPHAMs.Find(av.MaSP);
            Av_.TenSP = av.TenSP;
            Av_.DonGia = av.DonGia;
            Av_.SoLuong = av.SoLuong;
            Av_.NongDo = av.NongDo;
            Av_.NhomHuong = av.NhomHuong;
            Av_.MoTaSanPham = av.MoTaSanPham;
            Av_.NamPhatHanhSanPham = av.NamPhatHanhSanPham;
            Av_.AnhSanPham = av.AnhSanPham;
            Av_.MaNhanHieu = av.MaNhanHieu;
            Av_.MaLoai = av.MaLoai;
            Av_.DungTich = av.DungTich;
            dbSANPHAM.Entry(Av_).State = System.Data.Entity.EntityState.Modified;
            dbSANPHAM.SaveChanges();

            return "Actor Updated";
        }

        public string Delete(int id)
        {
            CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
            SANPHAM sp = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sp);
            db.SaveChanges();
            return "Deleted";
        }
    }
}
