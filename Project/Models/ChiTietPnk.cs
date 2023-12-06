using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class ChiTietPnk
{
    public string MaHang { get; set; } = null!;

    public int? SoLuong { get; set; }

    public string? DonViTinh { get; set; }

    public int? DonGia { get; set; }

    public string MaPnk { get; set; } = null!;

    public virtual Hang MaHangNavigation { get; set; } = null!;

    public virtual PhieuNhapKho MaPnkNavigation { get; set; } = null!;
}
