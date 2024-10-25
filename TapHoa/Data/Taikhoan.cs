using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Taikhoan
{
    public int Matk { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string Matkhau { get; set; } = null!;

    public string Chucvu { get; set; } = null!;

    public virtual ICollection<Khachhang> Khachhangs { get; set; } = new List<Khachhang>();

    public virtual ICollection<Nhanvien> Nhanviens { get; set; } = new List<Nhanvien>();
}
