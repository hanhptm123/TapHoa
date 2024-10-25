using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Chitietdondathang
{
    public int Masp { get; set; }

    public int Maddh { get; set; }

    public int Soluong { get; set; }

    public decimal Thanhtien { get; set; }

    public virtual Dondathang MaddhNavigation { get; set; } = null!;

    public virtual Sanpham MaspNavigation { get; set; } = null!;
}
