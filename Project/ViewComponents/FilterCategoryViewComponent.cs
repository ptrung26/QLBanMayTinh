using BTLWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTLWEB.ViewComponents
{
	public class FilterCategoryViewComponent : ViewComponent
	{
		QuanLyBanHang2Context db = new QuanLyBanHang2Context();
		public FilterCategoryViewComponent() { }
		public IViewComponentResult Invoke()
		{
			var lstCate = db.DanhMucs.ToList();
			return View(lstCate);
		}
	}
}
