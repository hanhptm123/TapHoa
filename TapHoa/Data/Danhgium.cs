using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Danhgium
{
    public int Madg { get; set; }

    public int? Masp { get; set; }

    public int? Makh { get; set; }

    public DateTime Ngaydanhgia { get; set; }

    public int Diemdanhgia { get; set; }

    public string Loidanhgia { get; set; } = null!;

    public string Hinhanh { get; set; } = null!;

    public virtual Khachhang? MakhNavigation { get; set; }

    public virtual Sanpham? MaspNavigation { get; set; }
}
