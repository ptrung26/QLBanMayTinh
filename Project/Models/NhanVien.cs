using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class NhanVien
{
    public string MaNv { get; set; } = null!;

    public string TenNv { get; set; } = null!;

    public string? Sdt { get; set; }

    public string? GioiTinh { get; set; }

    public DateTime? NgaySinh { get; set; }

    public DateTime? NgayBd { get; set; }

    public string? DiaChi { get; set; }

    public string MaChucVu { get; set; } = null!;

    public string? GhiChu { get; set; }

    public virtual ICollection<Hdban> Hdbans { get; } = new List<Hdban>();

    public virtual ICollection<Hdnhap> Hdnhaps { get; } = new List<Hdnhap>();

    public virtual ChucVu MaChucVuNavigation { get; set; } = null!;
}
