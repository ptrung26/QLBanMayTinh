using BTLWEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BTLWEB.Models.Authentication;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace BTLWEB.Controllers
{
	public class HomeController : Controller
	{
		#region Fields 
		private readonly ILogger<HomeController> _logger;
		private readonly QuanLyBanHangContext _context;
		#endregion

		#region Constructors
		public HomeController(ILogger<HomeController> logger, QuanLyBanHangContext context)
		{
			_logger = logger;
			_context = context;
		}
		#endregion

		#region Methods 

		/// <summary>
		/// Home
		/// </summary>
		/// <returns></returns>
		[Route("")]
		[Route("home")]
		public IActionResult Index()
		{
			return View();
		}

		[Route("product/{mahang}")]
		public IActionResult ChiTietSanPham(string mahang)
		{
			var lstAnh = _context.Anhs.Where(x => x.MaHang == mahang).ToList();
			ViewBag.lstAnh = lstAnh;
			var sp = _context.Hangs.SingleOrDefault(x => x.MaHang == mahang);
			var danhmuc = _context.DanhMucs.SingleOrDefault(x => x.MaDanhMuc == sp.MaDanhMuc);
			ViewBag.TenDanhMuc = danhmuc.TenDanhMuc;
			ViewBag.MaDanhMuc = danhmuc.MaDanhMuc;
			return View(sp);
		}

		[Route("danhmucsanpham")]
		public IActionResult DanhMucSanpham()
		{
			return View();
		}


		/// <summary>
		/// Go to Cart page 
		/// </summary>
		/// <returns></returns>
		[Route("cart")]
		public IActionResult GioHang()
		{
			return View();
		}


		/// <summary>
		/// Go to checkout page 
		/// </summary>
		/// <returns></returns>
		[Route("checkout")]
		public IActionResult Checkout()
		{
			return View();
		}

		/// <summary>
		/// Go to Error page 
		/// </summary>
		/// <returns></returns>
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		#endregion
	}
}