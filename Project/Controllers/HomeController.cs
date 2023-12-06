using BTLWEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BTLWEB.Models.Authentication;
using X.PagedList;

namespace BTLWEB.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private QuanLyBanHang2Context db = new QuanLyBanHang2Context();
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		[Route("")]
		[Route("home")]
		public IActionResult Index()
		{
			return View();
		}

		[Route("product/{mahang}")]
		public IActionResult ChiTietSanPham(string mahang)
		{
			var lstAnh = db.Anhs.Where(x => x.MaHang == mahang).ToList();
			ViewBag.lstAnh = lstAnh;
			var sp = db.Hangs.SingleOrDefault(x => x.MaHang == mahang);
			var danhmuc = db.DanhMucs.SingleOrDefault(x => x.MaDanhMuc == sp.MaDanhMuc);
			ViewBag.TenDanhMuc = danhmuc.TenDanhMuc;
			ViewBag.MaDanhMuc = danhmuc.MaDanhMuc;
			return View(sp);
		}

		[Route("danhmucsanpham")]
		public IActionResult DanhMucSanpham(string? query, string? madanhmuc, string? priceMin, string? priceMax, int page = 1)
		{
			IQueryable<Hang> lstSP = db.Hangs;

			int minPrice, maxPrice;
			minPrice = 0;
			maxPrice = lstSP.Max(p => p.DonGiaBan).Value;

			if (!string.IsNullOrEmpty(query))
			{
				lstSP = lstSP.Where(x => x.TenHang.ToLower().StartsWith(query.ToLower()));
			}
			ViewBag.priceMin = 0;
			ViewBag.priceMax = 0;
			if (priceMin != null)
			{
				ViewBag.priceMin = priceMin;
				int.TryParse(priceMin, out minPrice);
			}
			if (priceMax != null)
			{
				int.TryParse(priceMax, out maxPrice);
				ViewBag.priceMax = maxPrice;
			}
			if (!string.IsNullOrEmpty(madanhmuc))
			{
				string[] mdms = madanhmuc.Split(',');
				lstSP = lstSP.Where(x => mdms.Contains(x.MaDanhMuc));
				ViewBag.madanhmuc = madanhmuc;

			}

			lstSP = lstSP.Where(x => minPrice <= 0.7 * x.DonGiaBan && 0.7 * x.DonGiaBan <= maxPrice);
			int pageSize = 10;
			ViewBag.totalPages = lstSP.Count() <= pageSize ? 1 : (int)Math.Ceiling((double)lstSP.Count() / pageSize);
			ViewBag.currentPage = page;
			lstSP = lstSP.Skip((page - 1) * pageSize).Take(pageSize);
			return View(lstSP.ToList());
		}

		[Route("cart")]
		[Authentication]
		public IActionResult GioHang()
		{
			return View();
		}
		[Route("checkout")]
		public IActionResult Checkout()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}