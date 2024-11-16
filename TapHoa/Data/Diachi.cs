using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Diachi
{
    public int Madc { get; set; }

    public int? Makh { get; set; }

    public string Tennguoinhan { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public string Diachi1 { get; set; } = null!;

    public virtual Khachhang? MakhNavigation { get; set; }
}
