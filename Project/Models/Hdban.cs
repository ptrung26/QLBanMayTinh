using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class Hdban
{
    public string MaHdb { get; set; } = null!;

    public DateTime NgayLap { get; set; }

    public int? TongTien { get; set; }

    public string MaNv { get; set; } = null!;

    public string MaKh { get; set; } = null!;

    public virtual KhachHang MaKhNavigation { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
