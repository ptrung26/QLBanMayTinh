using BTLWEB.Models;
using BTLWEB.Models.API;
using BTLWEB.Services.Interfaces;
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
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResultService(success: false, message: ex.Message, statusCode: StatusHttpCode.ServerError));
            }

        }

        [HttpPost("register")]
        public IActionResult Register(TaiKhoanUser u)
        {
            try
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
            catch (Exception)
            {
                return StatusCode(500, new ActionResultService(success: false, message: "Server Error", statusCode: StatusHttpCode.ServerError));
            }
        }

        [HttpPost("forgot")]
        public async Task<IActionResult> ForgotPassword(string username)
        {
            try
            {
                await _accessServices.ForgotPassword(username);
                return Ok(new ActionResultService(success: true, message: "", statusCode: StatusHttpCode.Success));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResultService(success: false, message: ex.Message, statusCode: StatusHttpCode.ServerError));
            }

        }


        [HttpPost("reset_password")]
        public IActionResult ResetPassword(string username, string password)
        {
            try
            {
                var affectedRow = _accessServices.ResetPassword(username, password);
                if (affectedRow == 0)
                {
                    return BadRequest(new ActionResultService(success: false, message: "Bad Request", statusCode: StatusHttpCode.BadRequest));
                }
                return Ok(new ActionResultService(success: true, message: "", statusCode: StatusHttpCode.Success));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActionResultService(success: false, message: ex.Message, statusCode: StatusHttpCode.ServerError));
            }

        }

    }
}
