using BTLWEB.Models;
using BTLWEB.Models.API;
using BTLWEB.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace BTLWEB.Controllers.API
{
	[Route("api/access")]
	[ApiController]
	public class AccessAPI : ControllerBase
	{
		#region Fields
		private readonly IAccessServices _accessServices;

		#endregion

		#region Constructors 
		public AccessAPI(IAccessServices accessServices)
		{
			_accessServices = accessServices;
		}
		#endregion


		[HttpPost("login")]
		public IActionResult Login(UserDTO user)
		{
			var jwt = _accessServices.Login(user.UsernameLogin, user.PasswordLogin);
			if (string.IsNullOrEmpty(jwt))
			{
				return BadRequest(new ActionResultService(success: true, message: "Tài khoản hoặc mật khẩu không chính xác", statusCode: StatusHttpCode.ServerError));
			}
			return Ok(new ActionResultService(success: true, message: "", statusCode: StatusHttpCode.Success, new
			{
				jwt
			}));
		}

		[HttpPost("register")]
		public IActionResult Register(TaiKhoanUser u)
		{
			var jwt = _accessServices.Register(u);
			if (string.IsNullOrEmpty(jwt))
			{
				return BadRequest(new ActionResultService(success: true, message: "Bad Request", statusCode: StatusHttpCode.ServerError));
			}
			return StatusCode(201, new ActionResultService(success: true, message: "", statusCode: StatusHttpCode.Success, new
			{
				jwt
			}));
		}

		[HttpPost("forgot")]
		public async Task<IActionResult> ForgotPassword(string username)
		{
			await _accessServices.ForgotPassword(username);
			return Ok(new ActionResultService(success: true, message: "", statusCode: StatusHttpCode.Success));
		}


		[HttpPost("resetpassword")]
		public IActionResult ResetPassword(UserDTO user)
		{
			var affectedRow = _accessServices.ResetPassword(user.UsernameLogin, user.PasswordLogin);
			if (affectedRow == 0)
			{
				return BadRequest(new ActionResultService(success: false, message: "Bad Request", statusCode: StatusHttpCode.BadRequest));
			}
			return Ok(new ActionResultService(success: true, message: "", statusCode: StatusHttpCode.Success));
		}

		[HttpPost("getinfo")]
		[Authorize]
		public IActionResult GetInfo()
		{
			var username = HttpContext.User.FindFirstValue("Name");
			var user = _accessServices.GetInfo(username);
			return Ok(new ActionResultService(success: true, message: "", statusCode: StatusHttpCode.Success, user));
		}

		[HttpGet("get_info_customer")]
		[Authorize]
		public IActionResult GetInfoKH()
		{
			var username = HttpContext.User.FindFirstValue("Name");
			var kh = _accessServices.GetInfoKH(username);
			return Ok(new ActionResultService(success: true, message: "", statusCode: StatusHttpCode.Success, kh));
		}
	}
}
