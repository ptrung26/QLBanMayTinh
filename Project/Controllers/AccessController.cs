using BTLWEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BTLWEB.Controllers
{
    public class AccessController : Controller
    {
        #region Fields
        private readonly QuanLyBanHangContext _context;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructors 
        public AccessController(QuanLyBanHangContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        #endregion

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Forgot()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string username)
        {
            return View();
        }


    }
}
