using BTLWEB.Models;

namespace BTLWEB.Services
{
	public interface IProductServices
	{
		#region Methods 
		public List<Hang> GetNewProducts();
		public List<Hang> GetTopSellProducts();

		public Hang GetById(string id);
		public List<Hang> GetAllProductsRelated(string mahang);

		public object FilterProducts(string? query, string? madanhmuc, int? priceMin, int? priceMax, int page = 1);
		#endregion
	}
}
