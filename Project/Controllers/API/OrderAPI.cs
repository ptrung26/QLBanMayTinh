using BTLWEB.Models;
using BTLWEB.Models.API;
using BTLWEB.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTLWEB.Controllers.API
{
    [Route("api/order")]
    [ApiController]
    public class OrderAPI : ControllerBase
    {
        #region Fields 
        private readonly IOrderServices _orderServices;
        #endregion

        #region Constructors 
        public OrderAPI(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> AddNewOrder(List<ChiTietHdb> cthdbs)
        {
            var result = await _orderServices.Insert(cthdbs);
            if (result > 0)
            {
                return StatusCode(201, new ActionResultService(true, "", StatusHttpCode.Created));
            }
            else
            {
                return BadRequest(new ActionResultService(false, "Thêm không thành công", StatusHttpCode.BadRequest));
            }
        }

        public IActionResult GetOrderById(string id)
        {
            var hdb = _orderServices.GetOrderById(id);
            if (hdb == null)
            {
                return BadRequest(new ActionResultService(false, "Thêm không thành công", StatusHttpCode.BadRequest));
            }

            return Ok(new ActionResultService(true, "", StatusHttpCode.Success, hdb));
        }
    }
}
