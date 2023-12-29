using BTLWEB.Models;
using BTLWEB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTLWEB.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductAPI : ControllerBase
	{
		#region Fields 
		private readonly IProductServices _productsService;
		#endregion

		#region Constructors 
		public ProductAPI(IProductServices productServices)
		{
			_productsService = productServices;
		}
		#endregion

		#region Methods 
		[HttpGet("newproducts")]
		public List<Hang> GetNewProducts()
		{
			var lstSp = _productsService.GetNewProducts();
			return lstSp;
		}


		[HttpGet("topsellproducts")]
		public List<Hang> GetTopSellPrdoucts()
		{
			var lstSp = _productsService.GetTopSellProducts();
			return lstSp;
		}

		[HttpGet("{id}")]
		public Hang GetById(string id)
		{
			var sp = _productsService.GetById(id);
			return sp;
		}

		[HttpGet("related/{mahang}")]
		public List<Hang> GetAllProductsRelated(string mahang)
		{
			var lstSp = _productsService.GetAllProductsRelated(mahang);
			return lstSp;
		}
		#endregion

	}
}
