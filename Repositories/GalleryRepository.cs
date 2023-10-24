using Microsoft.EntityFrameworkCore;
using Shopping_Cart_Web_Application_V1._0.Data;
using Shopping_Cart_Web_Application_V1._0.Models;

namespace Shopping_Cart_Web_Application_V1._0.Repositories
{
	public class GalleryRepository:IGalleryRepository
	{
		private readonly ApplicationDbContext _db;
		public GalleryRepository(ApplicationDbContext db) 
		{
			_db = db;
		}
		public async Task<IEnumerable<Product>> GetProducts(string sTerm = "")
		{
            sTerm ??= string.Empty;
            sTerm = sTerm.ToLower();
			IEnumerable<Product> products = await (from product in _db.Product
											 where string.IsNullOrWhiteSpace(sTerm) || (product != null && product.ProductName.ToLower().Contains(sTerm))
											 select new Product
											 {
												 Id = product.Id,
												 Image = product.Image,
												 ProductName = product.ProductName,
												 ProductDescription = product.ProductDescription,
												 Price = product.Price
											 }
						).ToListAsync();
			return products;
		}	
	}
}
