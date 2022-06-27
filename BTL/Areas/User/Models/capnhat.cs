using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL.Areas.User.Models
{
    public class capnhat
    {
        public double Tongtien { get; set; }
        public double Tongsoluong { get; set; }
        public capnhat(double Tongtien, double Tongsoluong)
        {
            this.Tongtien = Tongtien;
            this.Tongsoluong = Tongsoluong;
        }
    }
}