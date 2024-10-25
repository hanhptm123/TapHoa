using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Phuongthucthanhtoan
{
    public int Mapttt { get; set; }

    public string Tenpttt { get; set; } = null!;

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();
}
