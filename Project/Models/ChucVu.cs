using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class ChucVu
{
    public string MaChucVu { get; set; } = null!;

    public string? TenChucVu { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; } = new List<NhanVien>();
}
