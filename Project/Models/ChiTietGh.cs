using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class ChiTietGh
{
    public string MaGh { get; set; } = null!;

    public string MaHang { get; set; } = null!;

    public int? SoLuong { get; set; }

    public int? DonGia { get; set; }

    public virtual GioHang MaGhNavigation { get; set; } = null!;

    public virtual Hang MaHangNavigation { get; set; } = null!;
}
