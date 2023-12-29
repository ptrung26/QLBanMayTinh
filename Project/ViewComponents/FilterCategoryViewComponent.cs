using BTLWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTLWEB.ViewComponents
{
	public class FilterCategoryViewComponent : ViewComponent
	{
		#region Fields 
		private readonly QuanLyBanHangContext _context;
		#endregion

		#region Constructors 
		public FilterCategoryViewComponent(QuanLyBanHangContext context)
		{
			_context = context;
		}
		#endregion

		#region Methods
		public IViewComponentResult Invoke()
		{
			var lstCate = _context.DanhMucs.ToList();
			return View(lstCate);
		}
		#endregion
	}
}
