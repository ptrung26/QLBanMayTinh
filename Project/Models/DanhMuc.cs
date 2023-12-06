using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class DanhMuc
{
    public string MaDanhMuc { get; set; } = null!;

    public string? TenDanhMuc { get; set; }

    public virtual ICollection<Hang> Hangs { get; } = new List<Hang>();
}
