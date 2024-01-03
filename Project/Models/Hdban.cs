using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class Hdban
{
	public string? MaHdb { get; set; }

	public DateTime NgayLap { get; set; } = DateTime.Now;

	public int? TongTien { get; set; }

	public string? MaNv { get; set; }

	public string? MaKh { get; set; }

	public virtual KhachHang MaKhNavigation { get; set; } = null!;

	public virtual NhanVien MaNvNavigation { get; set; }
}
