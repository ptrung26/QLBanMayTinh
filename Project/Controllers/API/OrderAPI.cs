using BTLWEB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTLWEB.Controllers.API
{
	[Route("api/order")]
	[ApiController]
	public class OrderAPI : ControllerBase
	{
		QuanLyBanHang2Context db = new QuanLyBanHang2Context();


		[HttpGet]
		public IActionResult GetAllOrder()
		{
			var lstOrders = db.Hdbans.ToList();
			var totalOrder = db.Hdbans.Count();

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
			var cart = db.GioHangs.SingleOrDefault(x => x.TenTaiKhoan.Equals(userName));
			if (cart == null)
			{
				return BadRequest(new { message = " object is null" });
			}

			try
			{
				db.Hdbans.Add(hdban);
				db.SaveChanges();
				return CreatedAtAction(nameof(getOrderById), new { id = hdban.MaHdb }, hdban);
			}
			catch
			{
				return BadRequest(new { message = "Add new order failed" });
			}

		}

		public IActionResult getOrderById(int id)
		{
			var hdb = db.Hdbans.SingleOrDefault(x => x.MaHdb.Equals(id));
			if (hdb == null)
			{
				return BadRequest(new { message = "object is null" });
			}

			return Ok(hdb);
		}
	}
}
