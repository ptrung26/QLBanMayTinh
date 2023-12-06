using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class GioHang
{
    public string MaGh { get; set; } = null!;

    public int? TongTien { get; set; }

    public string? TenTaiKhoan { get; set; }

    public virtual ICollection<ChiTietGh> ChiTietGhs { get; } = new List<ChiTietGh>();

    public virtual TaiKhoanUser? TenTaiKhoanNavigation { get; set; }
}
