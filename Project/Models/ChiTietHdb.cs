using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BTLWEB.Models;

public partial class ChiTietHdb
{
	public string? MaHdb { get; set; }

	public string MaHang { get; set; } = null!;

	public int? DonGia { get; set; }

	public int? SoLuong { get; set; }

	public decimal? ThanhTien { get; set; }

	[JsonIgnore]
	public virtual Hang? MaHangNavigation { get; set; }
	[JsonIgnore]
	public virtual Hdban? MaHdbNavigation { get; set; } 
}
