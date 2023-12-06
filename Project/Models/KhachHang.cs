using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class KhachHang
{
    public string MaKh { get; set; } = null!;

    public string TenKh { get; set; } = null!;

    public string? Sdt { get; set; }

    public string? DiaChi { get; set; }

    public string? Email { get; set; }

    public string? TenTaiKhoan { get; set; }

    public virtual ICollection<Hdban> Hdbans { get; } = new List<Hdban>();

    public virtual TaiKhoanUser? TenTaiKhoanNavigation { get; set; }
}
