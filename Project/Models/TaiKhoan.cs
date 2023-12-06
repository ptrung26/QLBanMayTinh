using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class TaiKhoan
{
    public string TenTk { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string MaBaoMat { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
