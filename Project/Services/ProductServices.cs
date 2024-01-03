using BTLWEB.Models;

namespace BTLWEB.Services
{
	public class ProductServices : IProductServices
	{
		#region Fields 
		private readonly QuanLyBanHangContext _context;
		#endregion
		#region Constructors 
		public ProductServices(QuanLyBanHangContext context)
		{
			_context = context;
		}
		#endregion

		#region Methods 
		public Hang GetById(string id)
		{
			var sp = _context.Hangs.SingleOrDefault(x => x.MaHang == id);
			return sp;
		}

		public List<Hang> GetNewProducts()
		{
			var list = (from h in _context.Hangs
						join ctpnks in _context.ChiTietPnks on h.MaHang equals ctpnks.MaHang
						join pnks in _context.PhieuNhapKhos on ctpnks.MaPnk equals pnks.MaPnk
						orderby pnks.NgayNhap descending
						select h).Take(5).ToList();
			return list;
		}

		public List<Hang> GetTopSellProducts()
		{
			var list = _context.Hangs.OrderByDescending(x => x.SoLanMua).Take(5).ToList();
			return list;
		}

		public List<Hang> GetAllProductsRelated(string mahang)
		{
			var sp = _context.Hangs.Single(x => x.MaHang == mahang);
			var lstSp = _context.Hangs.Where(x => x.MaDanhMuc == sp.MaDanhMuc).ToList();
			return lstSp;
		}

		public object FilterProducts(string? query, string? madanhmuc, int? priceMin, int? priceMax, int page = 1)
		{
			IQueryable<Hang> lstSP = _context.Hangs;

			if (!string.IsNullOrEmpty(query))
			{
				lstSP = lstSP.Where(x => x.TenHang.ToLower().StartsWith(query.ToLower()));
			}
			if (!string.IsNullOrEmpty(madanhmuc))
			{
				string[] mdms = madanhmuc.Split(',');
				lstSP = lstSP.Where(x => mdms.Contains(x.MaDanhMuc));
			}
			if (priceMin.HasValue && priceMax.HasValue)
			{
				int minPrice = priceMin.Value;
				int maxPrice = priceMax.Value;
				lstSP = lstSP.Where(x => x.DonGiaBan * 0.7 >= (float)(minPrice) && x.DonGiaBan <= (float)(maxPrice));
			}

			int pageSize = 5;
			int total = lstSP.Count();
			lstSP = lstSP.Skip((page - 1) * pageSize).Take(pageSize);
			return new
			{
				total,
				lstSP,
			};
		}


		#endregion
	}
}
