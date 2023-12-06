using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class BaoHanh
{
    public string MaBh { get; set; } = null!;

    public DateTime? NgayLap { get; set; }

    public DateTime? NgayTra { get; set; }

    public string MaKh { get; set; } = null!;

    public int? TongTien { get; set; }

    public string? GhiChu { get; set; }

    public virtual KhachHang MaKhNavigation { get; set; } = null!;
}
