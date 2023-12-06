using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class ChiTietPxk
{
    public string MaPxk { get; set; } = null!;

    public string MaHang { get; set; } = null!;

    public string? DonViTinh { get; set; }

    public int? SoLuong { get; set; }

    public int? DonGia { get; set; }

    public virtual Hang MaHangNavigation { get; set; } = null!;

    public virtual PhieuXuatKho MaPxkNavigation { get; set; } = null!;
}
