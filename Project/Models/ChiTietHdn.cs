using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class ChiTietHdn
{
    public string MaHdn { get; set; } = null!;

    public string MaHang { get; set; } = null!;

    public int? SoLuong { get; set; }

    public int? DonGia { get; set; }

    public int? ThanhTien { get; set; }

    public virtual Hang MaHangNavigation { get; set; } = null!;

    public virtual Hdnhap MaHdnNavigation { get; set; } = null!;
}
