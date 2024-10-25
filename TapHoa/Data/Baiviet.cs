using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Baiviet
{
    public int Mabv { get; set; }

    public int? Manv { get; set; }

    public string Tenbv { get; set; } = null!;

    public string Noidung { get; set; } = null!;

    public DateTime Ngaydang { get; set; }

    public string Hinhanh { get; set; } = null!;

    public virtual Nhanvien? ManvNavigation { get; set; }
}
