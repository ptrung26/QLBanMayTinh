using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTLWEB.Models;

public partial class QuanLyBanHangContext : DbContext
{


	public QuanLyBanHangContext()
	{
	}

	public QuanLyBanHangContext(DbContextOptions<QuanLyBanHangContext> options)
		: base(options)
	{

	}

	public virtual DbSet<Anh> Anhs { get; set; }

	public virtual DbSet<BaoHanh> BaoHanhs { get; set; }

	public virtual DbSet<ChiTietGh> ChiTietGhs { get; set; }

	public virtual DbSet<ChiTietHdb> ChiTietHdbs { get; set; }

	public virtual DbSet<ChiTietHdn> ChiTietHdns { get; set; }

	public virtual DbSet<ChiTietPnk> ChiTietPnks { get; set; }

	public virtual DbSet<ChiTietPxk> ChiTietPxks { get; set; }

	public virtual DbSet<ChucVu> ChucVus { get; set; }

	public virtual DbSet<DanhMuc> DanhMucs { get; set; }

	public virtual DbSet<GioHang> GioHangs { get; set; }

	public virtual DbSet<Hang> Hangs { get; set; }

	public virtual DbSet<Hdban> Hdbans { get; set; }

	public virtual DbSet<Hdnhap> Hdnhaps { get; set; }

	public virtual DbSet<KhachHang> KhachHangs { get; set; }

	public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

	public virtual DbSet<NhanVien> NhanViens { get; set; }

	public virtual DbSet<PhieuNhapKho> PhieuNhapKhos { get; set; }

	public virtual DbSet<PhieuXuatKho> PhieuXuatKhos { get; set; }

	public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

	public virtual DbSet<TaiKhoanUser> TaiKhoanUsers { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Anh>(entity =>
		{
			entity.HasKey(e => e.MaAnh).HasName("pk_anh");

			entity.ToTable("Anh");

			entity.Property(e => e.MaAnh)
				.HasMaxLength(10)
				.HasColumnName("maAnh");
			entity.Property(e => e.MaHang)
				.HasMaxLength(10)
				.HasColumnName("maHang");
			entity.Property(e => e.TenAnh)
				.HasMaxLength(10)
				.HasColumnName("tenAnh");

			entity.HasOne(d => d.MaHangNavigation).WithMany(p => p.Anhs)
				.HasForeignKey(d => d.MaHang)
				.HasConstraintName("fk_anh_hang");
		});

		modelBuilder.Entity<BaoHanh>(entity =>
		{
			entity
				.HasNoKey()
				.ToTable("BaoHanh");

			entity.Property(e => e.GhiChu).HasColumnType("text");
			entity.Property(e => e.MaBh)
				.HasMaxLength(10)
				.HasColumnName("MaBH");
			entity.Property(e => e.MaKh)
				.HasMaxLength(10)
				.HasColumnName("MaKH");
			entity.Property(e => e.NgayLap).HasColumnType("date");
			entity.Property(e => e.NgayTra).HasColumnType("date");

			entity.HasOne(d => d.MaKhNavigation).WithMany()
				.HasForeignKey(d => d.MaKh)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__BaoHanh__MaKH__5629CD9C");
		});

		modelBuilder.Entity<ChiTietGh>(entity =>
		{
			entity.HasKey(e => new { e.MaGh, e.MaHang }).HasName("pk_ctgh");

			entity.ToTable("ChiTietGH");

			entity.Property(e => e.MaGh)
				.HasMaxLength(10)
				.HasColumnName("maGH");
			entity.Property(e => e.MaHang)
				.HasMaxLength(10)
				.HasColumnName("maHang");

			entity.HasOne(d => d.MaGhNavigation).WithMany(p => p.ChiTietGhs)
				.HasForeignKey(d => d.MaGh)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("fk_ctgh_gh");

			entity.HasOne(d => d.MaHangNavigation).WithMany(p => p.ChiTietGhs)
				.HasForeignKey(d => d.MaHang)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("fk_ctgh_hang");
		});

		modelBuilder.Entity<ChiTietHdb>(entity =>
		{
			entity
				.HasNoKey()
				.ToTable("ChiTietHDB");

			entity.Property(e => e.MaHang).HasMaxLength(10);
			entity.Property(e => e.MaHdb)
				.HasMaxLength(10)
				.HasColumnName("MaHDB");
			entity.Property(e => e.ThanhTien).HasColumnType("money");

			entity.HasOne(d => d.MaHangNavigation).WithMany()
				.HasForeignKey(d => d.MaHang)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__ChiTietHD__MaHan__59063A47");

			entity.HasOne(d => d.MaHdbNavigation).WithMany()
				.HasForeignKey(d => d.MaHdb)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__ChiTietHD__MaHDB__59FA5E80");
		});

		modelBuilder.Entity<ChiTietHdn>(entity =>
		{
			entity
				.HasNoKey()
				.ToTable("ChiTietHDN");

			entity.Property(e => e.MaHang).HasMaxLength(10);
			entity.Property(e => e.MaHdn)
				.HasMaxLength(10)
				.HasColumnName("MaHDN");

			entity.HasOne(d => d.MaHangNavigation).WithMany()
				.HasForeignKey(d => d.MaHang)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__ChiTietHD__MaHan__5AEE82B9");

			entity.HasOne(d => d.MaHdnNavigation).WithMany()
				.HasForeignKey(d => d.MaHdn)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__ChiTietHD__MaHDN__5BE2A6F2");
		});

		modelBuilder.Entity<ChiTietPnk>(entity =>
		{
			entity
				.HasNoKey()
				.ToTable("ChiTietPNK");

			entity.Property(e => e.DonViTinh).HasMaxLength(255);
			entity.Property(e => e.MaHang).HasMaxLength(10);
			entity.Property(e => e.MaPnk)
				.HasMaxLength(10)
				.HasColumnName("MaPNK");

			entity.HasOne(d => d.MaHangNavigation).WithMany()
				.HasForeignKey(d => d.MaHang)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__ChiTietPN__MaHan__5CD6CB2B");

			entity.HasOne(d => d.MaPnkNavigation).WithMany()
				.HasForeignKey(d => d.MaPnk)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__ChiTietPN__MaPNK__5DCAEF64");
		});

		modelBuilder.Entity<ChiTietPxk>(entity =>
		{
			entity
				.HasNoKey()
				.ToTable("ChiTietPXK");

			entity.Property(e => e.DonViTinh).HasMaxLength(255);
			entity.Property(e => e.MaHang).HasMaxLength(10);
			entity.Property(e => e.MaPxk)
				.HasMaxLength(10)
				.HasColumnName("MaPXK");

			entity.HasOne(d => d.MaHangNavigation).WithMany()
				.HasForeignKey(d => d.MaHang)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__ChiTietPX__MaHan__5EBF139D");

			entity.HasOne(d => d.MaPxkNavigation).WithMany()
				.HasForeignKey(d => d.MaPxk)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__ChiTietPX__MaPXK__5FB337D6");
		});

		modelBuilder.Entity<ChucVu>(entity =>
		{
			entity.HasKey(e => e.MaChucVu);

			entity.ToTable("ChucVu");

			entity.Property(e => e.MaChucVu).HasMaxLength(10);
			entity.Property(e => e.TenChucVu).HasMaxLength(255);
		});

		modelBuilder.Entity<DanhMuc>(entity =>
		{
			entity.HasKey(e => e.MaDanhMuc).HasName("pk_danhmuc");

			entity.ToTable("DanhMuc");

			entity.Property(e => e.MaDanhMuc).HasMaxLength(10);
			entity.Property(e => e.TenDanhMuc).HasMaxLength(10);
		});

		modelBuilder.Entity<GioHang>(entity =>
		{
			entity.HasKey(e => e.MaGh).HasName("pk_giohang");

			entity.ToTable("GioHang");

			entity.Property(e => e.MaGh)
				.HasMaxLength(10)
				.HasColumnName("maGH");
			entity.Property(e => e.TenTaiKhoan).HasMaxLength(10);

			entity.HasOne(d => d.TenTaiKhoanNavigation).WithMany(p => p.GioHangs)
				.HasForeignKey(d => d.TenTaiKhoan)
				.HasConstraintName("FK__GioHang__TenTaiK__60A75C0F");
		});

		modelBuilder.Entity<Hang>(entity =>
		{
			entity.HasKey(e => e.MaHang).HasName("PK_tHang");

			entity.ToTable("Hang");

			entity.Property(e => e.MaHang).HasMaxLength(10);
			entity.Property(e => e.AnhDaiDien)
				.HasMaxLength(10)
				.HasColumnName("anhDaiDien");
			entity.Property(e => e.CardDoHoa).HasMaxLength(255);
			entity.Property(e => e.Cpu)
				.HasMaxLength(255)
				.HasColumnName("CPU");
			entity.Property(e => e.MaDanhMuc).HasMaxLength(10);
			entity.Property(e => e.ManHinh).HasMaxLength(255);
			entity.Property(e => e.MoTa).HasColumnType("text");
			entity.Property(e => e.Ocung)
				.HasMaxLength(255)
				.HasColumnName("OCung");
			entity.Property(e => e.Ram).HasMaxLength(255);
			entity.Property(e => e.Slton).HasColumnName("SLTon");
			entity.Property(e => e.SoLanMua).HasColumnName("soLanMua");
			entity.Property(e => e.TenHang).HasMaxLength(255);

			entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.Hangs)
				.HasForeignKey(d => d.MaDanhMuc)
				.HasConstraintName("FK_PersonOrder");
		});

		modelBuilder.Entity<Hdban>(entity =>
		{
			entity.HasKey(e => e.MaHdb);

			entity.ToTable("HDBan");

			entity.Property(e => e.MaHdb)
				.HasMaxLength(10)
				.HasColumnName("MaHDB");
			entity.Property(e => e.MaKh)
				.HasMaxLength(10)
				.HasColumnName("MaKH");
			entity.Property(e => e.MaNv)
				.HasMaxLength(10)
				.HasColumnName("MaNV");
			entity.Property(e => e.NgayLap).HasColumnType("date");

			entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.Hdbans)
				.HasForeignKey(d => d.MaKh)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__HDBan__MaKH__628FA481");

			entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Hdbans)
				.HasForeignKey(d => d.MaNv)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__HDBan__MaNV__6383C8BA");
		});

		modelBuilder.Entity<Hdnhap>(entity =>
		{
			entity.HasKey(e => e.MaHdn);

			entity.ToTable("HDNhap");

			entity.Property(e => e.MaHdn)
				.HasMaxLength(10)
				.HasColumnName("MaHDN");
			entity.Property(e => e.MaNcc)
				.HasMaxLength(10)
				.HasColumnName("MaNCC");
			entity.Property(e => e.MaNv)
				.HasMaxLength(10)
				.HasColumnName("MaNV");
			entity.Property(e => e.NgayNhap).HasColumnType("date");

			entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.Hdnhaps)
				.HasForeignKey(d => d.MaNcc)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__HDNhap__MaNCC__6477ECF3");

			entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Hdnhaps)
				.HasForeignKey(d => d.MaNv)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__HDNhap__MaNV__656C112C");
		});

		modelBuilder.Entity<KhachHang>(entity =>
		{
			entity.HasKey(e => e.MaKh);

			entity.ToTable("KhachHang");

			entity.Property(e => e.MaKh)
				.HasMaxLength(10)
				.HasColumnName("MaKH");
			entity.Property(e => e.DiaChi).HasMaxLength(255);
			entity.Property(e => e.Email).HasMaxLength(255);
			entity.Property(e => e.Sdt)
				.HasMaxLength(255)
				.HasColumnName("SDT");
			entity.Property(e => e.TenKh)
				.HasMaxLength(255)
				.HasColumnName("TenKH");
			entity.Property(e => e.TenTaiKhoan).HasMaxLength(10);

			entity.HasOne(d => d.TenTaiKhoanNavigation).WithMany(p => p.KhachHangs)
				.HasForeignKey(d => d.TenTaiKhoan)
				.HasConstraintName("FK_KhachHang_TaiKhoanUser");
		});

		modelBuilder.Entity<NhaCungCap>(entity =>
		{
			entity.HasKey(e => e.MaNcc).HasName("PK_tNCC");

			entity.ToTable("NhaCungCap");

			entity.Property(e => e.MaNcc)
				.HasMaxLength(10)
				.HasColumnName("MaNCC");
			entity.Property(e => e.DiaChi).HasMaxLength(255);
			entity.Property(e => e.Sdt)
				.HasMaxLength(255)
				.HasColumnName("SDT");
			entity.Property(e => e.TenNcc)
				.HasMaxLength(255)
				.HasColumnName("TenNCC");
		});

		modelBuilder.Entity<NhanVien>(entity =>
		{
			entity.HasKey(e => e.MaNv);

			entity.ToTable("NhanVien");

			entity.Property(e => e.MaNv)
				.HasMaxLength(10)
				.HasColumnName("MaNV");
			entity.Property(e => e.DiaChi).HasMaxLength(255);
			entity.Property(e => e.GhiChu).HasMaxLength(255);
			entity.Property(e => e.GioiTinh).HasMaxLength(10);
			entity.Property(e => e.MaChucVu).HasMaxLength(10);
			entity.Property(e => e.NgayBd)
				.HasColumnType("date")
				.HasColumnName("NgayBD");
			entity.Property(e => e.NgaySinh).HasColumnType("date");
			entity.Property(e => e.Sdt)
				.HasMaxLength(255)
				.HasColumnName("SDT");
			entity.Property(e => e.TenNv)
				.HasMaxLength(255)
				.HasColumnName("TenNV");

			entity.HasOne(d => d.MaChucVuNavigation).WithMany(p => p.NhanViens)
				.HasForeignKey(d => d.MaChucVu)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK__NhanVien__MaChuc__6754599E");
		});

		modelBuilder.Entity<PhieuNhapKho>(entity =>
		{
			entity.HasKey(e => e.MaPnk).HasName("PK_PhieuNhapKho_1");

			entity.ToTable("PhieuNhapKho");

			entity.Property(e => e.MaPnk)
				.HasMaxLength(10)
				.HasColumnName("MaPNK");
			entity.Property(e => e.MaNv)
				.HasMaxLength(10)
				.HasColumnName("MaNV");
			entity.Property(e => e.NgayNhap).HasColumnType("date");
		});

		modelBuilder.Entity<PhieuXuatKho>(entity =>
		{
			entity.HasKey(e => e.MaPxk);

			entity.ToTable("PhieuXuatKho");

			entity.Property(e => e.MaPxk)
				.HasMaxLength(10)
				.HasColumnName("MaPXK");
			entity.Property(e => e.MaNv)
				.HasMaxLength(10)
				.HasColumnName("MaNV");
			entity.Property(e => e.NgayXuat).HasColumnType("date");
		});

		modelBuilder.Entity<TaiKhoan>(entity =>
		{
			entity
				.HasNoKey()
				.ToTable("TaiKhoan");

			entity.Property(e => e.MaBaoMat).HasMaxLength(20);
			entity.Property(e => e.MaNv)
				.HasMaxLength(10)
				.HasColumnName("MaNV");
			entity.Property(e => e.MatKhau).HasMaxLength(20);
			entity.Property(e => e.TenTk)
				.HasMaxLength(20)
				.HasColumnName("TenTK");

			entity.HasOne(d => d.MaNvNavigation).WithMany()
				.HasForeignKey(d => d.MaNv)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("fk_taikhoan_nhanvien");
		});

		modelBuilder.Entity<TaiKhoanUser>(entity =>
		{
			entity.HasKey(e => e.TenTaiKhoan).HasName("pk_TaiKhoanUser");

			entity.ToTable("TaiKhoanUser");

			entity.Property(e => e.TenTaiKhoan).HasMaxLength(10);
			entity.Property(e => e.MatKhau).HasMaxLength(10);
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
