using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Chitiethoadon
{
    public int Masp { get; set; }

    public int Mahd { get; set; }

    public int Soluong { get; set; }

    public decimal Thanhtien { get; set; }

    public virtual Hoadon MahdNavigation { get; set; } = null!;

    public virtual Sanpham MaspNavigation { get; set; } = null!;
}
