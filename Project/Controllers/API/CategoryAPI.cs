using BTLWEB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTLWEB.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryAPI : ControllerBase
	{
		#region Fields 
		private readonly QuanLyBanHangContext _context;
		#endregion

		#region Constructors 
		public CategoryAPI(QuanLyBanHangContext context)
		{
			_context = context;
		}
		#endregion

		#region Methods 
		[HttpGet]
		public List<DanhMuc> GetAllCategories()
		{
			var lstDM = _context.DanhMucs.OrderBy(x => x.TenDanhMuc).ToList();
			return lstDM;
		}
		#endregion
	}
}
