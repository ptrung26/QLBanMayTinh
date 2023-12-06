using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class Hang
{
    public string MaHang { get; set; } = null!;

    public string TenHang { get; set; } = null!;

    public int? DonGiaNhap { get; set; }

    public int? DonGiaBan { get; set; }

    public string? Ram { get; set; }

    public string? Ocung { get; set; }

    public string? CardDoHoa { get; set; }

    public string? Cpu { get; set; }

    public string? ManHinh { get; set; }

    public int? Slton { get; set; }

    public int? ThoiGianBaoHanh { get; set; }

    public int? SoLanMua { get; set; }

    public string? MoTa { get; set; }

    public string? MaDanhMuc { get; set; }

    public string? AnhDaiDien { get; set; }

    public virtual ICollection<Anh> Anhs { get; } = new List<Anh>();

    public virtual ICollection<ChiTietGh> ChiTietGhs { get; } = new List<ChiTietGh>();

    public virtual DanhMuc? MaDanhMucNavigation { get; set; }
}
