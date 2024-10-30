using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
namespace TapHoa.Models
{
    public class Cartitem
    {
        public int Masanpham { get; set; }
        public string Tensanpham { get; set;}
        public string Hinh {  get; set; }
        public double Giasaugiam { get; set; }
        public double Giagoc { get; set; }
        public int Soluong { get; set; }
        public decimal Discount { get; set; }
        public double Thanhtien => Soluong * Giasaugiam;
    }
}
