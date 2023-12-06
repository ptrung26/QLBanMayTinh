using BTLWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTLWEB.Controllers
{
    public class AccessController : Controller
    {
        QuanLyBanHang2Context db = new QuanLyBanHang2Context();
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string? usernameLogin, string? passwordLogin)
        {
            if (string.IsNullOrEmpty(usernameLogin) || string.IsNullOrEmpty(passwordLogin))
            {
                return View();
            }

            if (HttpContext.Session.GetString("UserName") == null)
            {
                var user = db.TaiKhoanUsers.Where(x => x.TenTaiKhoan.Equals(usernameLogin) && x.MatKhau.Equals(passwordLogin)).FirstOrDefault();
                var admin = db.TaiKhoans.Where(x => x.TenTk.Equals(usernameLogin) && x.MatKhau.Equals(passwordLogin)).FirstOrDefault();
                if (user != null)
                {
                    HttpContext.Session.SetString("UserName", user.TenTaiKhoan.ToString());
                    return RedirectToAction("Index", "Home");
                }
                if (admin != null)
                {
                    HttpContext.Session.SetString("AdminName", admin.TenTk.ToString());
                    return RedirectToAction("DanhMucSanPham", "Admin");
                }

            }
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(TaiKhoanUser u)
        {
            try
            {
                db.TaiKhoanUsers.Add(u);
                db.SaveChanges();
                HttpContext.Session.SetString("UserName", u.TenTaiKhoan.ToString());
                var totalCart = db.GioHangs.Count();

                db.GioHangs.Add(new GioHang
                {
                    MaGh = "GH" + totalCart.ToString(),
                    TenTaiKhoan = u.TenTaiKhoan
                });
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }
    }
}
