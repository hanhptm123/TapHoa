using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Khachhang
{
    public int Makh { get; set; }

    public int? Matk { get; set; }

    public string Tenkh { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public string Diachi { get; set; } = null!;

    public virtual ICollection<Danhgium> Danhgia { get; set; } = new List<Danhgium>();

    public virtual ICollection<Diachi> Diachis { get; set; } = new List<Diachi>();

    public virtual ICollection<Dondathang> Dondathangs { get; set; } = new List<Dondathang>();

    public virtual Taikhoan? MatkNavigation { get; set; }
}
