using BTLWEB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTLWEB.Controllers.API
{
	[Route("api/order")]
	[ApiController]
	public class OrderAPI : ControllerBase
	{
		#region Fields 
		private readonly QuanLyBanHangContext _context;
		#endregion

		#region Constructors 
		public OrderAPI(QuanLyBanHangContext context)
		{
			_context = context;
		}
		#endregion


		[HttpGet]
		public IActionResult GetAllOrder()
		{
			var lstOrders = _context.Hdbans.ToList();
			var totalOrder = _context.Hdbans.Count();

			var response = new
			{
				data = lstOrders,
				total = totalOrder,
			};

			return Ok(response);
		}

		[HttpPost]
		public IActionResult addNewOrder(Hdban hdban)
		{
			if (hdban == null)
			{
				return BadRequest(new { message = "object is null" });
			}
			var userName = HttpContext.Session.GetString("UserName");
			if (userName == null)
			{
				return BadRequest(new { message = " object is null" });
			}
			var cart = _context.GioHangs.SingleOrDefault(x => x.TenTaiKhoan.Equals(userName));
			if (cart == null)
			{
				return BadRequest(new { message = " object is null" });
			}

			try
			{
				_context.Hdbans.Add(hdban);
				_context.SaveChanges();
				return CreatedAtAction(nameof(getOrderById), new { id = hdban.MaHdb }, hdban);
			}
			catch
			{
				return BadRequest(new { message = "Add new order failed" });
			}

		}

		public IActionResult getOrderById(int id)
		{
			var hdb = _context.Hdbans.SingleOrDefault(x => x.MaHdb.Equals(id));
			if (hdb == null)
			{
				return BadRequest(new { message = "object is null" });
			}

			return Ok(hdb);
		}
	}
}
