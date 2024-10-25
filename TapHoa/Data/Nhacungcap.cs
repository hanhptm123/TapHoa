using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Nhacungcap
{
    public int Mancc { get; set; }

    public string Tenncc { get; set; } = null!;

    public virtual ICollection<Sanpham> Sanphams { get; set; } = new List<Sanpham>();
}
