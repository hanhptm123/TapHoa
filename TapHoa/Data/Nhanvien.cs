using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Nhanvien
{
    public int Manv { get; set; }

    public int? Matk { get; set; }

    public string Tennv { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public string Diachi { get; set; } = null!;

    public virtual ICollection<Baiviet> Baiviets { get; set; } = new List<Baiviet>();

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();

    public virtual Taikhoan? MatkNavigation { get; set; }

    public virtual ICollection<Phieunhap> Phieunhaps { get; set; } = new List<Phieunhap>();
}
