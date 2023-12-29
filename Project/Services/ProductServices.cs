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
		#endregion
	}
}
