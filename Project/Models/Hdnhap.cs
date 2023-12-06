using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class Hdnhap
{
    public string MaHdn { get; set; } = null!;

    public DateTime? NgayNhap { get; set; }

    public int? TongTien { get; set; }

    public string MaNcc { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public virtual NhaCungCap MaNccNavigation { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
