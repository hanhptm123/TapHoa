using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Dondathang
{
    public int Maddh { get; set; }

    public int? Mattddh { get; set; }

    public int? Maptvc { get; set; }
    public int? Mapttt {  get; set; }

    public string Tenkh { get; set; } = null!;

    public string Diachi { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public DateTime Ngaydat { get; set; }

    public decimal? Tonggia { get; set; }

    public string? Lydohuy { get; set; }

    public int Makh { get; set; }

    public virtual ICollection<Chitietdondathang> Chitietdondathangs { get; set; } = new List<Chitietdondathang>();

    public virtual Khachhang MakhNavigation { get; set; } = null!;

    public virtual Phuongthucvanchuyen? MaptvcNavigation { get; set; }

    public virtual Trangthaidondathang? MattddhNavigation { get; set; }
    public virtual Phuongthucthanhtoan? MaptttNavigation {  get; set; }
}
