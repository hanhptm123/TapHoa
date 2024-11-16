using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Ctphieunhap
{
    public int Masp { get; set; }

    public int Mapn { get; set; }

    public int Soluong { get; set; }

    public decimal Thanhtien { get; set; }

    public virtual Phieunhap MapnNavigation { get; set; } = null!;

    public virtual Sanpham MaspNavigation { get; set; } = null!;
}
