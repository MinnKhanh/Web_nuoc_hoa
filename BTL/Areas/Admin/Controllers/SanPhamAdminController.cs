using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL;
using BTL.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using PagedList;
using Web_BTL.Areas.User.Models;
using Web_BTL.Areas.Admin.Models;

namespace Web_BTL.Areas.Admin.Controllers
{
    public class SanPhamAdminController : Controller
    { 
        CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
        // GET: Admin/SanPhamAdmin
       
        public ActionResult Index()
        {
            List<LOAI> DeptLoai = db.LOAIs.ToList();
            ViewBag.ListOfDepartment = new SelectList(DeptLoai, "MaLoai", "TenLoai");
            List<NHANHIEU> DeptNhanHieu = db.NHANHIEUx.ToList();
            ViewBag.ListOfDepartment1 = new SelectList(DeptNhanHieu, "MaNhanHieu", "TenNhanHieu");
            return View();
        }

        public JsonResult GetStudentList()
        {
            List<SanPhamViewModel> StuList = db.SANPHAMs.Select(x => new SanPhamViewModel()
            {
                MaSP = x.MaSP,
                TenSP = x.TenSP,
                DonGia = x.DonGia,
                SoLuong = x.SoLuong,
                NongDo = x.NongDo,
                NhomHuong = x.NhomHuong,
                MoTaSanPham = x.MoTaSanPham,
                NamPhatHanhSanPham = x.NamPhatHanhSanPham,
                AnhSanPham = x.AnhSanPham,
                TenNhanHieu = x.NHANHIEU.TenNhanHieu,
                TenLoai = x.LOAI.TenLoai,
                DungTich = x.DungTich
            }).ToList();

            return Json(StuList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentById(int StudentId)
        {
            SANPHAM model = db.SANPHAMs.Where(x => x.MaSP == StudentId).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(SANPHAM model , HttpPostedFileBase FileAnh)
        {
            var result = false;
            try
            {
                if (FileAnh != null)
                {
                    FileAnh.SaveAs(Server.MapPath("~/Resource/SanPham/") + FileAnh.FileName);
                    model.AnhSanPham = System.IO.Path.GetFileName(FileAnh.FileName);
                    if (model.MaSP > 0)
                    {
                        SANPHAM Stu = db.SANPHAMs.SingleOrDefault(x => x.MaSP == model.MaSP);
                        Stu.TenSP = model.TenSP;
                        Stu.DonGia = model.DonGia;
                        Stu.SoLuong = model.SoLuong;
                        Stu.NongDo = model.NongDo;
                        Stu.NhomHuong = model.NhomHuong;
                        Stu.MoTaSanPham = model.MoTaSanPham;
                        Stu.NamPhatHanhSanPham = model.NamPhatHanhSanPham;
                        Stu.MaNhanHieu = model.MaNhanHieu;
                        Stu.MaLoai = model.MaLoai;
                        db.SaveChanges();
                        result = true;
                    }
                    else
                    {
                        SANPHAM Stu = new SANPHAM();
                        Stu.TenSP = model.TenSP;
                        Stu.DonGia = model.DonGia;
                        Stu.SoLuong = model.SoLuong;
                        Stu.NongDo = model.NongDo;
                        Stu.NhomHuong = model.NhomHuong;
                        Stu.MoTaSanPham = model.MoTaSanPham;
                        Stu.NamPhatHanhSanPham = model.NamPhatHanhSanPham;
                        Stu.MaNhanHieu = model.MaNhanHieu;
                        Stu.MaLoai = model.MaLoai;
                        db.SANPHAMs.Add(Stu);
                        db.SaveChanges();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult ChuDePartial()
        {
            return PartialView(db.LOAIs.ToList());
        }

        public ActionResult SanPhamTheoLoai(int? page, int MaLoai = 1)
        {
            int pagesize = 6;
            int pagenumber = (page ?? 1);


            LOAI lsp = db.LOAIs.SingleOrDefault(n => n.MaLoai == MaLoai);
            if (lsp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<SANPHAM> lstSanPham = db.SANPHAMs.Where(n => n.MaLoai == MaLoai).OrderBy(n => n.TenSP).ToList();
            if (lstSanPham.Count == 0)
            {
                ViewBag.lstSanPham = "Không có sản phẩm nào";
            }
            ViewBag.MaLoai = MaLoai;
            ViewBag.lstSanPham = db.SANPHAMs.ToList();
            return View(lstSanPham.ToPagedList(pagenumber, pagesize));
        }

        public ViewResult ChiTietSanPham(int MaSp)
        {
            SANPHAM sanpham = db.SANPHAMs.Single(n => n.MaSP == MaSp);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }

        // Thêm sản phẩm:
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.MaNhanHieu = new SelectList(db.NHANHIEUx.ToList().OrderBy(n => n.TenNhanHieu), "MaNhanHieu",
                "TenNhanHieu");
            ViewBag.MaLoai = new SelectList(db.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai",
                "TenLoai");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(
            [Bind(Include =
                "MaSP , TenSP , DonGia , SoLuong , NongDo , NhomHuong , MoTaSanPham , NamPhatHanhSanPham , AnhSanPham , DungTich")]
            SANPHAM sp , HttpPostedFileBase FileAnh)
        {
            if (ModelState.IsValid)
            {
                if (FileAnh != null)
                {
                    FileAnh.SaveAs(Server.MapPath("~/Resource/SanPham/") + FileAnh.FileName);
                    sp.AnhSanPham = System.IO.Path.GetFileName(FileAnh.FileName);
                }
                db.SANPHAMs.Add(sp);
                db.SaveChanges();
                return RedirectToAction("Index", "SanPhamAdmin");
            }

            else
            {
                ViewBag.MaNhanHieu = new SelectList(db.NHANHIEUx.ToList().OrderBy(n => n.TenNhanHieu), "MaNhanHieu",
                    "TenNhanHieu");
                ViewBag.MaLoai = new SelectList(db.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai",
                    "TenLoai");
            }

            return View(sp);
        }

        // chỉnh sửa sản phẩm
        // Chỉnh sửa sản phẩm:
        [HttpGet]
        public ActionResult Update(string MaSp)
        {
            if (MaSp == null)
            {
                return HttpNotFound();
            }
            SANPHAM sanPham = db.SANPHAMs.Find(MaSp);
            if (sanPham == null)
            {
                return HttpNotFound();
            }


            ViewBag.MaNhanHieu = new SelectList(db.NHANHIEUx.ToList().OrderBy(n => n.TenNhanHieu), "MaNhanHieu",
                "TenNhanHieu");
            ViewBag.MaLoai = new SelectList(db.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai",
                "TenLoai");

            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = " MaSP , TenSP , DonGia , SoLuong , NongDo , NhomHuong , MoTaSanPham , NamPhatHanhSanPham , AnhSanPham , DungTich")] SANPHAM sanpham, HttpPostedFileBase FileAnh)
        {
            if (ModelState.IsValid)
            {
                if (FileAnh != null)
                {
                    FileAnh.SaveAs(Server.MapPath("~/Resource/SanPham/") + FileAnh.FileName);
                    sanpham.AnhSanPham = System.IO.Path.GetFileName(FileAnh.FileName);
                }
                db.Entry(sanpham).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.MaNhanHieu = new SelectList(db.NHANHIEUx.ToList().OrderBy(n => n.TenNhanHieu), "MaNhanHieu",
                    "TenNhanHieu");
                ViewBag.MaLoai = new SelectList(db.LOAIs.ToList().OrderBy(n => n.TenLoai), "MaLoai",
                    "TenLoai");
            }
            return RedirectToAction("Index");
        }

        // xóa sản phẩm
        [HttpGet]
        public ActionResult XoaSanPham(int MaSP)
        {
            SANPHAM sanpham = db.SANPHAMs.Single(n => n.MaSP == MaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }

        [HttpPost, ActionName("XoaSanPham")]
        public ActionResult XacNhanXoa(int MaSP)
        {
            SANPHAM sanpham = db.SANPHAMs.Single(n => n.MaSP == MaSP);
            var anhsp = from p in db.SANPHAMs
                where p.MaSP == sanpham.MaSP
                select p;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SANPHAMs.RemoveRange(anhsp);
            db.SANPHAMs.Remove(sanpham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
