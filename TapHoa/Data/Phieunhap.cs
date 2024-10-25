using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Phieunhap
{
    public int Mapn { get; set; }

    public int? Manv { get; set; }

    public DateTime Ngaynhap { get; set; }

    public decimal Tongtien { get; set; }

    public virtual ICollection<Ctphieunhap> Ctphieunhaps { get; set; } = new List<Ctphieunhap>();

    public virtual Nhanvien? ManvNavigation { get; set; }
}
