using BTLWEB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTLWEB.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductAPI : ControllerBase
	{
		QuanLyBanHang2Context db = new QuanLyBanHang2Context();
		[HttpGet("newproducts")]
		public List<Hang> GetNewProducts()
		{
			var lstSp = db.Hangs.Take(5).ToList();
			return lstSp;
		}


		[HttpGet("topsellproducts")]
		public List<Hang> getTopSellPrdoucts(int page = 1, int pageSize = 4)
		{
			var lstSp = db.Hangs.OrderBy(x => x.SoLanMua).Skip((page - 1) * pageSize).Take(pageSize).ToList();
			return lstSp;
		}

		[HttpGet("{id}")]
		public Hang getProductByID(string id)
		{
			var sp = db.Hangs.SingleOrDefault(x => x.MaHang == id);
			return sp;
		}

		[HttpGet("related/{mahang}")]
		public List<Hang> getAllProductsRelated(string mahang)
		{
			var sp = db.Hangs.Single(x => x.MaHang == mahang);
			var lstSp = db.Hangs.Where(x => x.MaDanhMuc == sp.MaDanhMuc).ToList();
			return lstSp;
		}

	}
}
