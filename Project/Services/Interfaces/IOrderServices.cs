using BTLWEB.Models;
using BTLWEB.Models.API;

namespace BTLWEB.Services.Interfaces
{
	public interface IOrderServices
	{
		public Task<int> Insert(List<ChiTietHdb> chiTietHdbs);
		public OrderDTO GetOrderById(string orderId);
	}
}
