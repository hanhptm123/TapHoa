using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Hoadon
{
    public int Mahd { get; set; }

    public int? Maptvc { get; set; }

    public int? Manv { get; set; }

    public int? Mapttt { get; set; }

    public DateTime? Ngaythanhtoan { get; set; }

    public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; } = new List<Chitiethoadon>();

    public virtual Nhanvien? ManvNavigation { get; set; }

    public virtual Phuongthucthanhtoan? MaptttNavigation { get; set; }

    public virtual Phuongthucvanchuyen? MaptvcNavigation { get; set; }
}
