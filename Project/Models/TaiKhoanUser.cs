using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class TaiKhoanUser
{
    public string TenTaiKhoan { get; set; } = null!;

    public string? MatKhau { get; set; }

    public virtual ICollection<GioHang> GioHangs { get; } = new List<GioHang>();

    public virtual ICollection<KhachHang> KhachHangs { get; } = new List<KhachHang>();
}
