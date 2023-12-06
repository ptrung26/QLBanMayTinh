using BTLWEB.Models;
using BTLWEB.Models.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTLWEB.Controllers.API
{
    [Route("api/cart")]
    [ApiController]
    public class CartAPI : ControllerBase
    {
        QuanLyBanHang2Context db = new QuanLyBanHang2Context();
        [HttpGet]
        public List<CTGH> getAllPrdouctsFromCart()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (userName == null)
            {
                return new List<CTGH>();
            }
            var cart = db.GioHangs.SingleOrDefault(x => x.TenTaiKhoan.Equals(userName.ToString()));
            if (cart == null)
            {
                return new List<CTGH>();
            }
            var lstCTGH = db.ChiTietGhs
                .Where(x => x.MaGh == cart.MaGh)
                .Select(x => new CTGH
                {
                    MaGh = cart.MaGh,
                    MaHang = x.MaHang,
                    TenHang = db.Hangs.Single(h => h.MaHang == x.MaHang).TenHang,
                    DonGiaBan = db.Hangs.Single(h => h.MaHang == x.MaHang).DonGiaBan,
                    SoLuong = x.SoLuong,
                    Anh = db.Hangs.Single(h => h.MaHang == x.MaHang).AnhDaiDien
                })
                .ToList();

            return lstCTGH;
        }

        [HttpPost]
        public bool AddProductToCart(string mahang, int? quantity)
        {
            var userName = HttpContext.Session.GetString("UserName");
            var cart = db.GioHangs.SingleOrDefault(x => x.TenTaiKhoan.Equals(userName.ToString()));
            if (cart == null || mahang == null || quantity == null)
                return false;

            var sp = db.ChiTietGhs.SingleOrDefault(x => x.MaGh == cart.MaGh && x.MaHang == mahang);
            if (sp == null)
            {
                var SP = db.Hangs.SingleOrDefault(x => x.MaHang == mahang);
                if (SP == null)
                {
                    return false;
                }

                db.ChiTietGhs.Add(new ChiTietGh()
                {
                    MaGh = cart.MaGh,
                    MaHang = mahang,
                    SoLuong = quantity,
                    DonGia = SP.DonGiaBan,
                });
            }
            else
            {
                sp.SoLuong += quantity;
                db.ChiTietGhs.Update(sp);
            }

            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpGet("{mahang}")]
        public bool DeleteProductFromCart(string? mahang)
        {
            if (mahang == null || mahang.Length == 0) { return false; }
            var userName = HttpContext.Session.GetString("UserName");
            var cart = db.GioHangs.SingleOrDefault(x => x.TenTaiKhoan.Equals(userName.ToString()));
            if (cart == null)
            {
                return false;
            }
            var sp = db.ChiTietGhs.SingleOrDefault(x => x.MaGh == cart.MaGh && x.MaHang == mahang);
            if (sp == null)
            {
                return false;
            }
            try
            {
                db.ChiTietGhs.Remove(sp);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
