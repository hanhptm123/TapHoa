using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TapHoa.Data;

public partial class TaphoaContext : DbContext
{
    public TaphoaContext()
    {
    }

    public TaphoaContext(DbContextOptions<TaphoaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Baiviet> Baiviets { get; set; }

    public virtual DbSet<Chitietdondathang> Chitietdondathangs { get; set; }

    public virtual DbSet<Chitiethoadon> Chitiethoadons { get; set; }

    public virtual DbSet<Ctphieunhap> Ctphieunhaps { get; set; }

    public virtual DbSet<Danhgium> Danhgia { get; set; }

    public virtual DbSet<Diachi> Diachis { get; set; }

    public virtual DbSet<Dondathang> Dondathangs { get; set; }

    public virtual DbSet<Hoadon> Hoadons { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<Khuyenmai> Khuyenmais { get; set; }

    public virtual DbSet<Loaisp> Loaisps { get; set; }

    public virtual DbSet<Nhacungcap> Nhacungcaps { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Phieunhap> Phieunhaps { get; set; }

    public virtual DbSet<Phuongthucthanhtoan> Phuongthucthanhtoans { get; set; }

    public virtual DbSet<Phuongthucvanchuyen> Phuongthucvanchuyens { get; set; }

    public virtual DbSet<Sanpham> Sanphams { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    public virtual DbSet<Thuonghieu> Thuonghieus { get; set; }

    public virtual DbSet<Trangthaidondathang> Trangthaidondathangs { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baiviet>(entity =>
        {
            entity.HasKey(e => e.Mabv).HasName("PK__BAIVIET__603FFF97144EC40F");

            entity.ToTable("BAIVIET");

            entity.Property(e => e.Mabv).HasColumnName("MABV");
            entity.Property(e => e.Hinhanh).HasColumnName("HINHANH");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Ngaydang)
                .HasColumnType("datetime")
                .HasColumnName("NGAYDANG");
            entity.Property(e => e.Noidung).HasColumnName("NOIDUNG");
            entity.Property(e => e.Tenbv).HasColumnName("TENBV");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.Baiviets)
                .HasForeignKey(d => d.Manv)
                .HasConstraintName("FK_BAIVIET_NHANVIEN");
        });

        modelBuilder.Entity<Chitietdondathang>(entity =>
        {
            entity.HasKey(e => new { e.Masp, e.Maddh });

            entity.ToTable("CHITIETDONDATHANG");

            entity.Property(e => e.Masp).HasColumnName("MASP");
            entity.Property(e => e.Maddh).HasColumnName("MADDH");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");
            entity.Property(e => e.Thanhtien)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("THANHTIEN");

            entity.HasOne(d => d.MaddhNavigation).WithMany(p => p.Chitietdondathangs)
                .HasForeignKey(d => d.Maddh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETDONDATHANG_DONDATHANG");

            entity.HasOne(d => d.MaspNavigation).WithMany(p => p.Chitietdondathangs)
                .HasForeignKey(d => d.Masp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETDONDATHANG_SANPHAM");
        });

        modelBuilder.Entity<Chitiethoadon>(entity =>
        {
            entity.HasKey(e => new { e.Masp, e.Mahd });

            entity.ToTable("CHITIETHOADON");

            entity.Property(e => e.Masp).HasColumnName("MASP");
            entity.Property(e => e.Mahd).HasColumnName("MAHD");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");
            entity.Property(e => e.Thanhtien)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("THANHTIEN");

            entity.HasOne(d => d.MahdNavigation).WithMany(p => p.Chitiethoadons)
                .HasForeignKey(d => d.Mahd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETHOADON_HOADON");

            entity.HasOne(d => d.MaspNavigation).WithMany(p => p.Chitiethoadons)
                .HasForeignKey(d => d.Masp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETHOADON_SANPHAM");
        });

        modelBuilder.Entity<Ctphieunhap>(entity =>
        {
            entity.HasKey(e => new { e.Masp, e.Mapn });

            entity.ToTable("CTPHIEUNHAP");

            entity.Property(e => e.Masp).HasColumnName("MASP");
            entity.Property(e => e.Mapn).HasColumnName("MAPN");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");
            entity.Property(e => e.Thanhtien)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("THANHTIEN");

            entity.HasOne(d => d.MapnNavigation).WithMany(p => p.Ctphieunhaps)
                .HasForeignKey(d => d.Mapn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTPHIEUNHAP_PHIEUNHAP");

            entity.HasOne(d => d.MaspNavigation).WithMany(p => p.Ctphieunhaps)
                .HasForeignKey(d => d.Masp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTPHIEUNHAP_SANPHAM");
        });

        modelBuilder.Entity<Danhgium>(entity =>
        {
            entity.HasKey(e => e.Madg).HasName("PK__DANHGIA__603F0046E3BA5481");

            entity.ToTable("DANHGIA");

            entity.Property(e => e.Madg).HasColumnName("MADG");
            entity.Property(e => e.Diemdanhgia).HasColumnName("DIEMDANHGIA");
            entity.Property(e => e.Hinhanh).HasColumnName("HINHANH");
            entity.Property(e => e.Loidanhgia).HasColumnName("LOIDANHGIA");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Masp).HasColumnName("MASP");
            entity.Property(e => e.Ngaydanhgia)
                .HasColumnType("datetime")
                .HasColumnName("NGAYDANHGIA");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Danhgia)
                .HasForeignKey(d => d.Makh)
                .HasConstraintName("FK_DANHGIA_KHACHHANG");

            entity.HasOne(d => d.MaspNavigation).WithMany(p => p.Danhgia)
                .HasForeignKey(d => d.Masp)
                .HasConstraintName("FK_DANHGIA_SANPHAM");
        });

        modelBuilder.Entity<Diachi>(entity =>
        {
            entity.HasKey(e => e.Madc).HasName("PK__DIACHI__603F004AE86B96A6");

            entity.ToTable("DIACHI");

            entity.Property(e => e.Madc).HasColumnName("MADC");
            entity.Property(e => e.Diachi1)
                .HasMaxLength(50)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.Tennguoinhan)
                .HasMaxLength(50)
                .HasColumnName("TENNGUOINHAN");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Diachis)
                .HasForeignKey(d => d.Makh)
                .HasConstraintName("FK_DIACHI_KHACHHANG");
        });

        modelBuilder.Entity<Dondathang>(entity =>
        {
            entity.HasKey(e => e.Maddh).HasName("PK__DONDATHA__77CD19D1A416CFAD");

            entity.ToTable("DONDATHANG");

            entity.Property(e => e.Maddh).HasColumnName("MADDH");
            entity.Property(e => e.Diachi).HasColumnName("DIACHI");
            entity.Property(e => e.Lydohuy).HasColumnName("LYDOHUY");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Maptvc).HasColumnName("MAPTVC");
            entity.Property(e => e.Mattddh).HasColumnName("MATTDDH");
            entity.Property(e => e.Mapttt).HasColumnName("MAPTTT");
            entity.Property(e => e.Ngaydat)
                .HasColumnType("datetime")
                .HasColumnName("NGAYDAT");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.Tenkh).HasColumnName("TENKH");
            entity.Property(e => e.Tonggia)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TONGGIA");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Dondathangs)
                .HasForeignKey(d => d.Makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DONDATHANG_KHACHHANG");

            entity.HasOne(d => d.MaptvcNavigation).WithMany(p => p.Dondathangs)
                .HasForeignKey(d => d.Maptvc)
                .HasConstraintName("FK_DONDATHANG_PHUONGTHUCVANCHUYEN");

            entity.HasOne(d => d.MattddhNavigation).WithMany(p => p.Dondathangs)
                .HasForeignKey(d => d.Mattddh)
                .HasConstraintName("FK_DONDATHANG_TRANGTHAIDONDATHANG");

            entity.HasOne(d => d.MaptttNavigation).WithMany(p => p.Dondathangs)
                .HasForeignKey(d => d.Mapttt)
                .HasConstraintName("FK_DONDATHANG_PHUONGTHUCTHANHTOAN");
        });


        modelBuilder.Entity<Hoadon>(entity =>
        {
            entity.HasKey(e => e.Mahd).HasName("PK__HOADON__603F20CEA5A13655");

            entity.ToTable("HOADON");

            entity.Property(e => e.Mahd).HasColumnName("MAHD");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Mapttt).HasColumnName("MAPTTT");
            entity.Property(e => e.Maptvc).HasColumnName("MAPTVC");
            entity.Property(e => e.Ngaythanhtoan)
                .HasColumnType("datetime")
                .HasColumnName("NGAYTHANHTOAN");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.Manv)
                .HasConstraintName("FK_HOADON_NHANVIEN");

            entity.HasOne(d => d.MaptttNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.Mapttt)
                .HasConstraintName("FK_HOADON_PHUONGTHUCTHANHTOAN");

            entity.HasOne(d => d.MaptvcNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.Maptvc)
                .HasConstraintName("FK_HOADON_PHUONGTHUCVANCHUYEN");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.Makh).HasName("PK__KHACHHAN__603F592CC79B5492");

            entity.ToTable("KHACHHANG");

            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Diachi)
                .HasMaxLength(50)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Matk).HasColumnName("MATK");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.Tenkh)
                .HasMaxLength(50)
                .HasColumnName("TENKH");

            entity.HasOne(d => d.MatkNavigation).WithMany(p => p.Khachhangs)
                .HasForeignKey(d => d.Matk)
                .HasConstraintName("FK_KHACHHANG_TAIKHOAN");
        });

        modelBuilder.Entity<Khuyenmai>(entity =>
        {
            entity.HasKey(e => e.Makm).HasName("PK__KHUYENMA__603F592B7FE84825");

            entity.ToTable("KHUYENMAI");

            entity.Property(e => e.Makm).HasColumnName("MAKM");
            entity.Property(e => e.Phantramgiam).HasColumnName("PHANTRAMGIAM");
        });

        modelBuilder.Entity<Loaisp>(entity =>
        {
            entity.HasKey(e => e.Maloaisp).HasName("PK__LOAISP__AFCB0D36E0629B7B");

            entity.ToTable("LOAISP");

            entity.Property(e => e.Maloaisp).HasColumnName("MALOAISP");
            entity.Property(e => e.Tenloaisp)
                .HasMaxLength(50)
                .HasColumnName("TENLOAISP");
        });

        modelBuilder.Entity<Nhacungcap>(entity =>
        {
            entity.HasKey(e => e.Mancc).HasName("PK__NHACUNGC__7ABEA5825C7CDC5B");

            entity.ToTable("NHACUNGCAP");

            entity.Property(e => e.Mancc).HasColumnName("MANCC");
            entity.Property(e => e.Tenncc)
                .HasMaxLength(50)
                .HasColumnName("TENNCC");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.Manv).HasName("PK__NHANVIEN__603F5114AE9F89AA");

            entity.ToTable("NHANVIEN");

            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Diachi)
                .HasMaxLength(50)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Matk).HasColumnName("MATK");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.Tennv)
                .HasMaxLength(50)
                .HasColumnName("TENNV");

            entity.HasOne(d => d.MatkNavigation).WithMany(p => p.Nhanviens)
                .HasForeignKey(d => d.Matk)
                .HasConstraintName("FK_NHANVIEN_TAIKHOAN");
        });

        modelBuilder.Entity<Phieunhap>(entity =>
        {
            entity.HasKey(e => e.Mapn).HasName("PK__PHIEUNHA__603F61CECFC07843");

            entity.ToTable("PHIEUNHAP");

            entity.Property(e => e.Mapn).HasColumnName("MAPN");
            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Ngaynhap)
                .HasColumnType("datetime")
                .HasColumnName("NGAYNHAP");
            entity.Property(e => e.Tongtien)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TONGTIEN");

            entity.HasOne(d => d.ManvNavigation).WithMany(p => p.Phieunhaps)
                .HasForeignKey(d => d.Manv)
                .HasConstraintName("FK_PHIEUNHAP_NHANVIEN");
        });

        modelBuilder.Entity<Phuongthucthanhtoan>(entity =>
        {
            entity.HasKey(e => e.Mapttt).HasName("PK__PHUONGTH__4F6B743E6B633EC0");

            entity.ToTable("PHUONGTHUCTHANHTOAN");

            entity.Property(e => e.Mapttt).HasColumnName("MAPTTT");
            entity.Property(e => e.Tenpttt).HasColumnName("TENPTTT");
        });

        modelBuilder.Entity<Phuongthucvanchuyen>(entity =>
        {
            entity.HasKey(e => e.Maptvc).HasName("PK__PHUONGTH__4F6B640D94163374");

            entity.ToTable("PHUONGTHUCVANCHUYEN");

            entity.Property(e => e.Maptvc).HasColumnName("MAPTVC");
            entity.Property(e => e.Tenptvc).HasColumnName("TENPTVC");
        });

        modelBuilder.Entity<Sanpham>(entity =>
        {
            entity.HasKey(e => e.Masp).HasName("PK__SANPHAM__60228A32B89EF2D1");

            entity.ToTable("SANPHAM");

            entity.Property(e => e.Masp).HasColumnName("MASP");
            entity.Property(e => e.Gia)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("GIA");
            entity.Property(e => e.Hinhanh).HasColumnName("HINHANH");
            entity.Property(e => e.Hinhanh1).HasColumnName("HINHANH1");
            entity.Property(e => e.Hinhanh2).HasColumnName("HINHANH2");
            entity.Property(e => e.Hinhanh3).HasColumnName("HINHANH3");
            entity.Property(e => e.Makm).HasColumnName("MAKM");
            entity.Property(e => e.Maloaisp).HasColumnName("MALOAISP");
            entity.Property(e => e.Mancc).HasColumnName("MANCC");
            entity.Property(e => e.Math).HasColumnName("MATH");
            entity.Property(e => e.Mota).HasColumnName("MOTA");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");
            entity.Property(e => e.Tensp)
                .HasMaxLength(50)
                .HasColumnName("TENSP");

            entity.HasOne(d => d.MakmNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Makm)
                .HasConstraintName("FK_SANPHAM_KHUYENMAI");

            entity.HasOne(d => d.MaloaispNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Maloaisp)
                .HasConstraintName("FK_SANPHAM_LOAISP");

            entity.HasOne(d => d.ManccNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Mancc)
                .HasConstraintName("FK_SANPHAM_NHACUNGCAP");

            entity.HasOne(d => d.MathNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Math)
                .HasConstraintName("FK_SANPHAM_THUONGHIEU");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.Matk).HasName("PK__TAIKHOAN__602372160C1C7285");

            entity.ToTable("TAIKHOAN");

            entity.Property(e => e.Matk).HasColumnName("MATK");
            entity.Property(e => e.Chucvu)
                .HasMaxLength(50)
                .HasColumnName("CHUCVU");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(50)
                .HasColumnName("MATKHAU");
            entity.Property(e => e.Tendangnhap)
                .HasMaxLength(50)
                .HasColumnName("TENDANGNHAP");
        });

        modelBuilder.Entity<Thuonghieu>(entity =>
        {
            entity.HasKey(e => e.Mathuonghieu).HasName("PK__THUONGHI__B319F638300F5C04");

            entity.ToTable("THUONGHIEU");

            entity.Property(e => e.Mathuonghieu).HasColumnName("MATHUONGHIEU");
            entity.Property(e => e.Tenthuonghieu)
                .HasMaxLength(50)
                .HasColumnName("TENTHUONGHIEU");
        });

        modelBuilder.Entity<Trangthaidondathang>(entity =>
        {
            entity.HasKey(e => e.Mattddh).HasName("PK__TRANGTHA__B1AE97250909D025");

            entity.ToTable("TRANGTHAIDONDATHANG");

            entity.Property(e => e.Mattddh).HasColumnName("MATTDDH");
            entity.Property(e => e.Tenttddh)
                .HasMaxLength(50)
                .HasColumnName("TENTTDDH");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
