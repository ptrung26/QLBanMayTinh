using BTLWEB.Controllers.Middlewares;
using BTLWEB.Models;
using BTLWEB.Models.API;
using BTLWEB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace BTLWEB.Services
{
    public class AccessServices : IAccessServices
    {
        #region Fields
        private readonly QuanLyBanHangContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailServices _emailServices;
        #endregion

        #region Constructors 
        public AccessServices(QuanLyBanHangContext context, IConfiguration configuration, IEmailServices emailServices)
        {
            _context = context;
            _configuration = configuration;
            _emailServices = emailServices;
        }
        #endregion

        private string GenerateJSONWebToken(string userName)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim("Name", userName),
                new Claim(JwtRegisteredClaimNames.Sub,userName),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        [HttpPost("login")]
        public string Login(string? usernameLogin, string? passwordLogin)
        {
            if (string.IsNullOrEmpty(usernameLogin) || string.IsNullOrEmpty(passwordLogin))
            {
                throw new BadRequestException(
                     "Tài khoản hoặc mật khẩu không được bỏ trống"
                );
            }
            var user = _context.TaiKhoanUsers.Where(x => x.TenTaiKhoan.Equals(usernameLogin) && x.MatKhau.Equals(passwordLogin)).FirstOrDefault();
            if (user == null)
            {
                throw new BadRequestException("Tài khoản hoặc mật khẩu không chính xác");
            }
            var admin = _context.TaiKhoans.Where(x => x.TenTk.Equals(usernameLogin) && x.MatKhau.Equals(passwordLogin)).FirstOrDefault();
            string jwtToken = "";
            if (user != null)
            {
                jwtToken = GenerateJSONWebToken(user.TenTaiKhoan);

            }
            if (admin != null)
            {
                jwtToken = GenerateJSONWebToken(admin.TenTk);
            }

            return jwtToken;

        }

        [HttpPost("register")]
        public string Register(TaiKhoanUser u)
        {
            if (string.IsNullOrEmpty(u.TenTaiKhoan) || string.IsNullOrEmpty(u.MatKhau))
            {
                throw new BadRequestException(
                     "Tài khoản hoặc mật khẩu không được bỏ trống"
                );
            }
            var user = _context.TaiKhoanUsers.FirstOrDefault(tk => tk.TenTaiKhoan == u.TenTaiKhoan);
            if (user != null)
            {
                throw new BadRequestException(
                     "Tài khoản đã tồn tại!"
                );
            }
            _context.TaiKhoanUsers.Add(u);
            _context.SaveChanges();
            string jwtToken = GenerateJSONWebToken(u.TenTaiKhoan);
            var totalCart = _context.GioHangs.Count();

            _context.GioHangs.Add(new GioHang
            {
                MaGh = "GH" + totalCart.ToString(),
                TenTaiKhoan = u.TenTaiKhoan
            });
            _context.SaveChanges();
            return jwtToken;
        }

        [HttpPost("forgot")]
        public async Task ForgotPassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new BadRequestException(
                     "Tài khoản không được bỏ trống"
                );
            }
            var user = await _context.TaiKhoanUsers.FirstOrDefaultAsync<TaiKhoanUser>(x => x.TenTaiKhoan.Equals(username));
            if (user == null)
            {
                throw new BadRequestException(
                     "Tài khoản không tồn tại"
                );
            }
            var kh = await _context.KhachHangs.FirstOrDefaultAsync<KhachHang>(kh => kh.TenTaiKhoan == user.TenTaiKhoan);
            if (string.IsNullOrEmpty(kh?.Email))
            {
                throw new BadRequestException(
                                 "Tài khoản khách hàng chưa cập nhật email!"
                );
            }
            var jwtToken = GenerateJSONWebToken(user.TenTaiKhoan);
            string requestLink = $"https://localhost:7019/access/resetpassword?username={kh.TenTaiKhoan}&token={jwtToken}";
            string messageBody = $"Dear User,<br /><br />"
                           + $"We received a request to reset your password. Please click the link below to reset your password:<br /><br />"
                           + $"<a style='color:blue;text-decoration: underline;cursor:pointer' href=\"{requestLink}\">{requestLink}</a><br /><br />"
                           + $"If you didn't request this, you can safely ignore this email.<br /><br />"
                           + $"Regards,<br />Your App";
            await _emailServices.SendEmailAsync(kh.Email, "Quên mật khẩu", messageBody);

        }

        public int ResetPassword(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new BadRequestException(
                     "Tài khoản hoặc mật khẩu không được bỏ trống"
                );
            }

            var user = _context.TaiKhoanUsers.FirstOrDefault<TaiKhoanUser>(u => u.TenTaiKhoan == username);
            if (user == null)
            {
                throw new BadRequestException(
                                 "Tài khoản không tồn tại"
                );
            }

            user.MatKhau = password;
            _context.SaveChanges();
            return 1;
        }

        public UserDTO GetInfo(string username)
        {
            var user = _context.TaiKhoanUsers.FirstOrDefault(item => item.TenTaiKhoan == username);
            if (user == null)
            {
                throw new BadRequestException("Tài khoản không tồn tại");
            }

            return new UserDTO
            {
                UsernameLogin = username,
            };
        }

        public KhachHang GetInfoKH(string username)
        {
            var kh = _context.KhachHangs.FirstOrDefault(item => item.TenTaiKhoan == username);
            if (kh == null)
            {
                throw new BadRequestException("Tài khoản không tồn tại");
            }

            return kh;
        }
    }
}
