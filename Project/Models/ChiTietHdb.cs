using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class ChiTietHdb
{
    public string MaHdb { get; set; } = null!;

    public string MaHang { get; set; } = null!;

    public int? DonGia { get; set; }

    public int? SoLuong { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual Hang MaHangNavigation { get; set; } = null!;

    public virtual Hdban MaHdbNavigation { get; set; } = null!;
}
