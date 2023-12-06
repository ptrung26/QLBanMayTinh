using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class Anh
{
    public string MaAnh { get; set; } = null!;

    public string? TenAnh { get; set; }

    public string? MaHang { get; set; }

    public virtual Hang? MaHangNavigation { get; set; }
}
