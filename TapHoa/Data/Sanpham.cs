using System;
using System.Collections.Generic;

namespace TapHoa.Data;

public partial class Sanpham
{
    public int Masp { get; set; }

    public int? Maloaisp { get; set; }

    public int? Makm { get; set; }

    public int? Mancc { get; set; }

    public int? Math { get; set; }

    public string Tensp { get; set; } = null!;

    public string Mota { get; set; } = null!;

    public int? Soluong { get; set; }

    public decimal Gia { get; set; }

    public string Hinhanh { get; set; } = null!;

    public string Hinhanh1 { get; set; } = null!;

    public string Hinhanh2 { get; set; } = null!;

    public string Hinhanh3 { get; set; } = null!;

    public virtual ICollection<Chitietdondathang> Chitietdondathangs { get; set; } = new List<Chitietdondathang>();

    public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; } = new List<Chitiethoadon>();

    public virtual ICollection<Ctphieunhap> Ctphieunhaps { get; set; } = new List<Ctphieunhap>();

    public virtual ICollection<Danhgium> Danhgia { get; set; } = new List<Danhgium>();

    public virtual Khuyenmai? MakmNavigation { get; set; }

    public virtual Loaisp? MaloaispNavigation { get; set; }

    public virtual Nhacungcap? ManccNavigation { get; set; }

    public virtual Thuonghieu? MathNavigation { get; set; }
}
