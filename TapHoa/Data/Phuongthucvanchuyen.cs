using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Phuongthucvanchuyen
{
    public int Maptvc { get; set; }

    public string Tenptvc { get; set; } = null!;

    public virtual ICollection<Dondathang> Dondathangs { get; set; } = new List<Dondathang>();

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();
}
