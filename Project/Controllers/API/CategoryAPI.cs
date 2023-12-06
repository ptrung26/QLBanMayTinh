using BTLWEB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTLWEB.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryAPI : ControllerBase
	{
		QuanLyBanHang2Context db = new QuanLyBanHang2Context();

		[HttpGet]
		public List<DanhMuc> GetAllCategories()
		{
			var lstDM = db.DanhMucs.OrderBy(x => x.TenDanhMuc).ToList();
			return lstDM;
		}
	}
}
