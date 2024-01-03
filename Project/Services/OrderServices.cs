using BTLWEB.Controllers.Middlewares;
using BTLWEB.Models;
using BTLWEB.Models.API;
using BTLWEB.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BTLWEB.Services
{
    public class OrderServices : IOrderServices
    {
        #region Fields 
        private readonly QuanLyBanHangContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailServices _emailServices;
        #endregion

        #region Constructors 
        public OrderServices(QuanLyBanHangContext context, IHttpContextAccessor httpContextAccessor, IEmailServices emailServices)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _emailServices = emailServices;
        }

        #endregion

        #region Methods

        public async Task<int> Insert(List<ChiTietHdb> chiTietHdbs)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (httpContext != null)
                {
                    var tenTK = httpContext.User.FindFirstValue("Name");
                    var maxHDB = _context.Hdbans.Count();
                    var kh = _context.KhachHangs.FirstOrDefault(kh => kh.TenTaiKhoan == tenTK);
                    Hdban hdb = new()
                    {
                        MaHdb = $"HDB{maxHDB + 1}",
                        MaKh = kh.MaKh,
                        NgayLap = DateTime.Now
                    };
                    _context.Hdbans.Add(hdb);
                    await _context.SaveChangesAsync();
                    foreach (var cthdb in chiTietHdbs)
                    {
                        cthdb.MaHdb = $"HDB{maxHDB + 1}";
                    }
                    _context.BulkInsert<ChiTietHdb>(chiTietHdbs);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    string body = "";
                    body += $"PaymentId: {hdb.MaHdb}<br/>FullName: {kh.TenKh}<br/>Product details:<br/>";
                    int totalPrice = 0;
                    foreach (var cthdb in chiTietHdbs)
                    {
                        var hang = _context.Hangs.FirstOrDefault(x => x.MaHang == cthdb.MaHang);
                        body += $"ProductId: {cthdb.MaHang}, Tên SP: {hang.TenHang}, Số lượng: {cthdb.SoLuong}, Đơn giá: {cthdb.DonGia}<br/>";
                        totalPrice += cthdb.SoLuong.Value * cthdb.DonGia.Value;
                    }

                    body += $"Tổng tiền: {totalPrice:#,###}";
                    await _emailServices.SendEmailAsync(kh.Email, "Place order successs", body);
                    return 1;



                }


                return -1;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new BadRequestException(ex.Message);
            }
        }

        public OrderDTO GetOrderById(string orderId)
        {
            var hdBan = _context.Hdbans.FirstOrDefault(item => item.MaHdb == orderId);
            var cthdbs = _context.ChiTietHdbs.Where(item => item.MaHdb == orderId).ToList();
            var order = new OrderDTO()
            {
                Hdban = hdBan,
                ChiTietHdbs = cthdbs
            };

            return order;
        }
        #endregion
    }
}
