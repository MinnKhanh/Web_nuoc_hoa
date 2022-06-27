using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL.Models
{
    public class CapNhatCart
    {
        public double Tongtien { get; set; }
        public double Tongsoluong { get; set; }
        public CapNhatCart(double Tongtien, double Tongsoluong)
        {
            this.Tongtien = Tongtien;
            this.Tongsoluong = Tongsoluong;
        }
    }
}