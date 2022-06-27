using BTL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BTL.Controllers
{
    public class BrandsApiController : ApiController
    {
        CSDL_QuanLyCuaHangBanNuocHoa db = new CSDL_QuanLyCuaHangBanNuocHoa();
        public IEnumerable<NHANHIEU> Get()
        {
            db.Configuration.ProxyCreationEnabled = false;
            // return BrandModel.GetAll();
            return db.NHANHIEUx.ToList();

        }

        // GET api/<controller>/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var entity = db.NHANHIEUx.FirstOrDefault(e => e.MaNhanHieu == id);
            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "Nhanhieu  = " + id.ToString() + "khong tim thay");
            }
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] NHANHIEU nhanhieu)
        {
            db.Configuration.ProxyCreationEnabled = false;
            try
            {
                // string dem = @"select COUNT(TenNhanHieu) from NHANHIEU where TenNhanHieu = '";
                db.NHANHIEUx.Add(nhanhieu);
                db.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, nhanhieu);
                message.Headers.Location = new Uri(Request.RequestUri + nhanhieu.MaNhanHieu.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody] NHANHIEU nhanhieu)
        {
            db.Configuration.ProxyCreationEnabled = false;
            try
            {
                var entity = db.NHANHIEUx.FirstOrDefault(e => e.MaNhanHieu == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Nhanhieu  = " + id.ToString() + " khong tim thay de xoa");

                }
                else
                {
                    entity.TenNhanHieu = nhanhieu.TenNhanHieu;
                    entity.Email = nhanhieu.Email;
                    entity.XuatXu = nhanhieu.XuatXu;
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            try
            {
                var entity = db.NHANHIEUx.FirstOrDefault(e => e.MaNhanHieu == id);
                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Nhanhieu  = " + id.ToString() + " khong tim thay de xoa");

                }
                else
                {
                    db.NHANHIEUx.Remove(entity);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
